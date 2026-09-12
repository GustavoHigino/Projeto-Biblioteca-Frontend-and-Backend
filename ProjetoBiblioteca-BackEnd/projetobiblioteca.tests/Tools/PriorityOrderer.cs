using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace projetobiblioteca.tests.Tools
{
    // Esta classe implementa a interface ITestCaseOrderer do xUnit.
    // Ela serve para controlar e mudar a ORDEM em que os testes são executados (já que por padrão o xUnit roda em ordem aleatória).
    public class PriorityOrderer : ITestCaseOrderer
    {
        // Este é o método obrigatório da interface que recebe a lista de testes não ordenados 
        // e precisa retornar essa mesma lista organizada na sequência desejada.
        public IEnumerable<TTestCase> 
            OrderTestCases<TTestCase>
            (IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            // Usa o LINQ (.OrderBy) para reordenar a lista de métodos de teste
            var sortedMethods = testCases.OrderBy(
                tc =>
                {
                    // Para cada método de teste, ele procura se existe o nosso atributo customizado [TestPriority(...)]
                    var attr = tc.TestMethod
                    .Method.GetCustomAttributes(
                        typeof(
                        TestPriorityAttribute))
                    .FirstOrDefault();

                    // Se o teste NÃO tiver a anotação [TestPriority], ele ganha prioridade padrão 0 (roda primeiro ou junto com os de prioridade 0).
                    if (attr == null) return 0;

                    // Extrai os argumentos passados no construtor do atributo (ex: o número 0 em [TestPriority(0)])
                    var constructorArgs = attr.
                    GetConstructorArguments();
                    // Se houver algum argumento no construtor, pega o primeiro valor e converte para número inteiro (int)
                    if (constructorArgs != null &&
                    constructorArgs.Any())
                    {
                        return (int)constructorArgs.First();
                    }
                    // Caso não venha via construtor positional, tenta buscar uma propriedade nomeada chamada "Priority"
                    //caso a pessoa atribua diretamente a propriedade é pego tambem
                    return attr.GetNamedArgument<int>
                    ("Priority");
                });
            // Retorna a lista de testes devidamente ordenada do menor número de prioridade para o maior (0, 1, 2...)
            return sortedMethods;
        }
    }
    // Cria a nossa anotação personalizada chamada [TestPriority(...)], que herda da classe base 'Attribute' do C#
    public class TestPriorityAttribute : Attribute
    {
        // Propriedade apenas de leitura para guardar o número da prioridade do teste
        public int Priority { get;}
        // O construtor obriga a passar um número inteiro quando usamos o atributo (ex: [TestPriority(1)])
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
