# 🧠 AutoMapper Genérico
Este projeto é uma **biblioteca de mapeamento objeto-para-objeto** desenvolvida em .NET, com o objetivo de realizar cópias profundas (**deep copy**) entre objetos complexos — sem depender de bibliotecas de terceiros como AutoMapper.

## ✨ Funcionalidades

- 🔁 Mapeamento de propriedades simples
- 🧩 Suporte a objetos aninhados e grafos complexos
- 📚 Mapeamento de listas e coleções aninhadas
- 🚫 Ignorar propriedades com atributo `[IgnoreMap]`
- 🔄 Atualização de objetos existentes (map de source em um destination já instanciado)
- ⚡ Alto desempenho com uso de **Expression Trees** e cache de delegates

## 📁 Estrutura da Solução

/AutoMapper → Biblioteca principal com mapeadores e interfaces /Tests → Testes unitários com xUnit /Benchmarker → Projeto de benchmark com BenchmarkDotNet


## 🧪 Testes

A solução possui uma suíte de testes automatizados com `xUnit`:

```bash
dotnet test
```

# 🚀 Benchmarks

Benchmarks de performance com `BenchmarkDotNet`, comparando o mapper manual com um mapper externo ou de sua preferência:

```bash
cd Benchmarker
dotnet run -c Release
```

# 📌 Exemplos de Uso

```csharp
using WMapper.Interfaces;

class Servico 
{
    private readonly IMapper _mapper;

    public Servico(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void Handler()
    {
        // Mapeando para novo objeto
        var dto = _mapper.Map<Person, PersonDto>(pessoa);

        // Atualizando objeto existente
        _mapper.Map(pessoa, pessoaDtoExistente);

        // Mapeando lista
        var listaPessoas = new List<listaDto>();
        List<listaDto> result = _mapper.Map<Person, PersonDto>(listaPessoas);

        // Mapeando lista de listas
        var listaPessoas = new List<List<listaDto>>();
        List<List<listaDto>> result = _mapper.Map<Person, PersonDto>(listaPessoas);
    }
}
```

# ✅ Requisitos

- .NET 6.0 ou superior

# 📦 NuGet

```bash
dotnet add package WMapper.Generic
```

```csharp
// Recomenda-se injetar no conteiner DI da aplicação e utilziar-lo ia injeção de dependência.
// Opções de escopo: Scoped, Transient, Singleton
using WMapper;

builder.Services.AddAutoMapper() //Scoped
builder.Services.AddAutoMapperSingleton() //Singleton
builder.Services.AddAutoMapperTransient() //Transient

// Você também pode instanciar manualmente a classe concreta:
var mapper = new Mapper();
```

# 📝 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

# 📊 Benchmarks

Você pode consultar os resultados dos benchmarks na pasta `BenchmarkDotNet.Artifacts` gerada após rodar os testes de performance.
