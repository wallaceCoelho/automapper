using System.Collections;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Annotations;
using AutoMapper.Models;

namespace AutoMapper.Mappers;

public abstract class BaseMapper
{
    private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyAccessor>> AccessorCache = new();
    private static readonly ConcurrentDictionary<Type, Func<object>> ConstructorCache = new();
    protected static readonly ConcurrentDictionary<(Type, Type), Action<object, object>> MapCache = new();
    
    protected static Action<object, object> BuildMapAction(Type sourceType, Type destinationType)
    {
        var sourceAccessors = GetAccessors(sourceType);
        var destinationAccessors = GetAccessors(destinationType);

        return (source, destination) =>
        {
            foreach (var (name, srcAccessor) in sourceAccessors)
            {
                if (!destinationAccessors.TryGetValue(name, out var destAccessor))
                    continue;

                var sourceValue = srcAccessor.Getter(source);
                if (sourceValue == null) continue;

                var srcType = srcAccessor.PropertyType;
                var destType = destAccessor.PropertyType;

                // Atribuição direta para tipos simples
                if (IsSimpleType(srcType) && destType.IsAssignableFrom(srcType))
                    destAccessor.Setter(destination, sourceValue);
                // Lista ou array
                else if (typeof(IEnumerable).IsAssignableFrom(srcType) && srcType != typeof(string))
                {
                    var sourceEnumerable = (IEnumerable)sourceValue;
                    var destItemType = GetElementType(destType);
                    var listType = typeof(List<>).MakeGenericType(destItemType!);
                    var destList = (IList?)CreateInstance(listType);
                    if (destList == null) continue;

                    foreach (var item in sourceEnumerable)
                    {
                        if (item == null) continue;

                        object mappedItem;
                        if (IsSimpleType(item.GetType()))
                            mappedItem = item;
                        else
                        {
                            var targetItem = CreateInstance(destItemType);
                            var itemMap = MapCache.GetOrAdd((item.GetType(), destItemType), _ => BuildMapAction(item.GetType(), destItemType));
                            itemMap(item, targetItem);
                            mappedItem = targetItem;
                        }
                        destList.Add(mappedItem);
                    }
                    destAccessor.Setter(destination, destList);
                }
                // Objeto complexo
                else
                {
                    var nestedTarget = CreateInstance(destType);
                    var nestedMap = MapCache.GetOrAdd((srcType, destType), _ => BuildMapAction(srcType, destType));
                    nestedMap(sourceValue, nestedTarget);
                    destAccessor.Setter(destination, nestedTarget);
                }
            }
        };
    }
    
    private static object CreateInstance(Type type) => ConstructorCache.GetOrAdd(type, t =>
        {
            var newExpr = Expression.New(t);
            var lambda = Expression.Lambda<Func<object>>(Expression.Convert(newExpr, typeof(object)));
            return lambda.Compile();
        })();

    private static Dictionary<string, PropertyAccessor> GetAccessors(Type type) => AccessorCache.GetOrAdd(type, t =>
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                         .Where(p => p is { CanRead: true, CanWrite: true } && 
                                     p.GetIndexParameters().Length == 0 &&
                                     !Attribute.IsDefined(p, typeof(IgnoreMapAttribute)));

            var dict = new Dictionary<string, PropertyAccessor>();
            foreach (var prop in props)
            {
                var getter = BuildGetter(t, prop);
                var setter = BuildSetter(t, prop);
                dict[prop.Name] = new PropertyAccessor
                {
                    Getter = getter,
                    Setter = setter,
                    PropertyType = prop.PropertyType
                };
            }

            return dict;
        });

    private static Func<object, object?> BuildGetter(Type type, PropertyInfo prop)
    {
        var obj = Expression.Parameter(typeof(object), "obj");
        var convert = Expression.Convert(obj, type);
        var property = Expression.Property(convert, prop);
        var cast = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<object, object?>>(cast, obj).Compile();
    }

    private static Action<object, object?> BuildSetter(Type type, PropertyInfo prop)
    {
        var obj = Expression.Parameter(typeof(object), "obj");
        var value = Expression.Parameter(typeof(object), "value");
        var convertObj = Expression.Convert(obj, type);
        var convertVal = Expression.Convert(value, prop.PropertyType);
        var assign = Expression.Assign(Expression.Property(convertObj, prop), convertVal);
        return Expression.Lambda<Action<object, object?>>(assign, obj, value).Compile();
    }

    private static Type GetElementType(Type type)
    {
        if (type.IsArray)
            return type.GetElementType()!;
        return type.IsGenericType ? type.GetGenericArguments()[0] : typeof(object);
    }

    private static bool IsSimpleType(Type type) => type.IsPrimitive || type.IsEnum || type == typeof(string)
            || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid);
}