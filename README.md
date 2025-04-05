# 🧠 AutoMapper Manual (.NET)

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

# 📌 Benchmarks

```csharp
// Recomenda-se injetar a interface no construtor da classe
private readonly IMapper _mapper = new Mapper();

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
```

# ✅ Requisitos

- .NET 6.0 ou superior

# 📦 NuGet (futuramente)

```bash
dotnet add package AutoMapper.Manual
```

# 📝 Licença

Este projeto está licenciado sob a MIT License.

# 📊 Benchmarks

Você pode consultar os resultados dos benchmarks na pasta `BenchmarkDotNet.Artifacts` gerada após rodar os testes de performance.
