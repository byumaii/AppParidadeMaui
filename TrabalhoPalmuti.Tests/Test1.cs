using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.ViewModels;

namespace TrabalhoPalmuti.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FiltrarCapitais_ComTermoExistente_RetornaListaFiltrada()
        {
            var viewModel = new MainViewModel();
            viewModel._listaCompletaCapitais.Add(new Capital { Nome = "São Paulo", EstadoSigla = "SP" });
            viewModel._listaCompletaCapitais.Add(new Capital { Nome = "Salvador", EstadoSigla = "BA" });
            viewModel._listaCompletaCapitais.Add(new Capital { Nome = "Rio de Janeiro", EstadoSigla = "RJ" });
            viewModel.FiltrarCapitais("salvador");

            Assert.AreEqual(1, viewModel.Capitais.Count);
            Assert.AreEqual("Salvador", viewModel.Capitais[0].Nome);
        }
    }
}