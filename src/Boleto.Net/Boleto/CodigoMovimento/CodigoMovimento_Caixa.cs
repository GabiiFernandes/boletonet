using System;
using System.Collections.Generic;
using System.Text;
using BoletoNet.Enums;

namespace BoletoNet
{

    #region Enumerado

    public enum EnumCodigoMovimento_Caixa_Legado
    {
        EntradaConfirmada = 2,
        EntradaRejeitada = 3,
        TransferenciaCarteiraEntrada = 4,
        TransferenciaCarteiraBaixa = 5,
        Liquidacao = 6,
        Baixa = 9,
        TitulosCarteiraEmSer = 11,
        ConfirmacaoRecebimentoInstrucaoAbatimento = 12,
        ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento = 13,
        ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento = 14,
        FrancoPagamento = 15,
        LiquidacaoAposBaixa = 17,
        ConfirmacaoRecebimentoInstrucaoProtesto = 19,
        ConfirmacaoRecebimentoInstrucaoSustacaoProtesto = 20,
        RemessaCartorio = 23,
        RetiradaCartorioManutencaoCarteira = 24,
        ProtestadoBaixado = 25,
        InstrucaoRejeitada = 26,
        ConfirmaçãoPedidoAlteracaoOutrosDados = 27,
        DebitoTarifas = 28,
        OcorrenciaSacado = 29,
        AlteracaoDadosRejeitada = 30,

    }

    public enum EnumCodigoMovimento_Caixa
    {
        //NOVOS COMANDOS 
        EntradaConfirmada = 01,
        BaixaManualConfirmada = 02,
        AbatimentoConcedido = 03,
        AbatimentoCancelado = 104,
        VencimentoAlterado = 05,
        UsoEmpresaAlterado = 06,
        PrazoProtestoAlterado = 07,
        PrazoDevolucaoAlterado = 08,
        AlteracaoConfirmada = 09,
        AlteracaoReemissaoBoletoConfirmada = 10,
        AlteracaoProtestoPDevolucaoConfirmada = 11,
        AlteracaoDevolucaoPProtestoConfirmada = 12,
        EmSer = 20,
        Liquidacao = 21,
        LiquidacaoEmCartorio = 22,
        BaixaPorDevolucao = 23,
        BaixaPorProtesto = 25,
        TituloEnviadoCartorio = 26,
        SustacaoProtesto = 27,
        EstornoProtesto = 28,
        EstornoSustacaoProtesto = 29,
        AlteracaoDoTitulo = 30,
        TarifaSobreTituloVencido = 31,
        OutrasTarifasDeAlteracao = 32,
        TarifasDiversas = 34, //valor total das tarifas cobradas, exceto de liquidação
        LiquidacaoOnline = 35,
        TranferenciaParaCobrancaSimples = 37,
        TranferenciaParaCobrancaDescontada = 38,
        ReconhecidoPeloPagadorDDA = 51,
        NaoReconhecidoPeloPagadorDDA = 52,
        RecusadoNoDDA = 53,
        RejeicaoDoTitulo = 99, //Código de rejeição informado nas pos 80 a 82
        //PagadorDDA = A4,
    }
    #endregion

    public class CodigoMovimento_Caixa : AbstractCodigoMovimento, ICodigoMovimento
    {
        bool legado = false;

        #region Construtores

        public CodigoMovimento_Caixa()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public CodigoMovimento_Caixa(int codigo)
        {
            try
            {
                this.carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public CodigoMovimento_Caixa(int codigo, bool legado)
        {
            try
            {
                this.legado = true;
                this.carregarLegado(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }
        #endregion

        #region Metodos Privados


        private void carregar(int codigo)
        {
            try
            {
                this.Banco = new Banco_Caixa();

                switch ((EnumCodigoMovimento_Caixa)codigo)
                {
                    case EnumCodigoMovimento_Caixa.EntradaConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.EntradaConfirmada;
                        this.Descricao = "Entrada confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.BaixaManualConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.BaixaManualConfirmada;
                        this.Descricao = "Baixa manual confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.AbatimentoConcedido:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AbatimentoConcedido;
                        this.Descricao = "Abatimento concedido";
                        break;
                    case EnumCodigoMovimento_Caixa.AbatimentoCancelado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AbatimentoCancelado;
                        this.Descricao = "Abatimento cancelado";
                        break;
                    case EnumCodigoMovimento_Caixa.VencimentoAlterado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.VencimentoAlterado;
                        this.Descricao = "Vencimento alterado";
                        break;
                    case EnumCodigoMovimento_Caixa.UsoEmpresaAlterado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.UsoEmpresaAlterado;
                        this.Descricao = "Uso da empresa alterado";
                        break;
                    case EnumCodigoMovimento_Caixa.PrazoProtestoAlterado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.PrazoProtestoAlterado;
                        this.Descricao = "Prazo de protesto alterado";
                        break;
                    case EnumCodigoMovimento_Caixa.PrazoDevolucaoAlterado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.PrazoDevolucaoAlterado;
                        this.Descricao = "Prazo de devolução alterado";
                        break;
                    case EnumCodigoMovimento_Caixa.AlteracaoConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AlteracaoConfirmada;
                        this.Descricao = "Alteração confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.AlteracaoReemissaoBoletoConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AlteracaoReemissaoBoletoConfirmada;
                        this.Descricao = "Alteração com reemissão de boleto confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.AlteracaoProtestoPDevolucaoConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AlteracaoProtestoPDevolucaoConfirmada;
                        this.Descricao = "Alteração da opção de protesto para devolução confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.AlteracaoDevolucaoPProtestoConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AlteracaoDevolucaoPProtestoConfirmada;
                        this.Descricao = "Alteração da opção de devolução para protesto confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa.EmSer:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.EmSer;
                        this.Descricao = "Em ser";
                        break;
                    case EnumCodigoMovimento_Caixa.Liquidacao:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.Liquidacao;
                        this.Descricao = "Liquidação";
                        break;
                    case EnumCodigoMovimento_Caixa.LiquidacaoEmCartorio:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.LiquidacaoEmCartorio;
                        this.Descricao = "Liquidação em cartório";
                        break;
                    case EnumCodigoMovimento_Caixa.BaixaPorDevolucao:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.BaixaPorDevolucao;
                        this.Descricao = "Baixa por devolução";
                        break;
                    case EnumCodigoMovimento_Caixa.BaixaPorProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.BaixaPorProtesto;
                        this.Descricao = "Baixa por protesto";
                        break;
                    case EnumCodigoMovimento_Caixa.TituloEnviadoCartorio:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.TituloEnviadoCartorio;
                        this.Descricao = "Título enviado para cartório";
                        break;
                    case EnumCodigoMovimento_Caixa.SustacaoProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.SustacaoProtesto;
                        this.Descricao = "Sustação de protesto";
                        break;
                    case EnumCodigoMovimento_Caixa.EstornoProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.EstornoProtesto;
                        this.Descricao = "Estorno de protesto";
                        break;
                    case EnumCodigoMovimento_Caixa.EstornoSustacaoProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.EstornoSustacaoProtesto;
                        this.Descricao = "Estorno sustação de protesto";
                        break;
                    case EnumCodigoMovimento_Caixa.AlteracaoDoTitulo:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.AlteracaoDoTitulo;
                        this.Descricao = "Alteração de título";
                        break;
                    case EnumCodigoMovimento_Caixa.TarifaSobreTituloVencido:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.TarifaSobreTituloVencido;
                        this.Descricao = "Tarifa sobre título vencido";
                        break;
                    case EnumCodigoMovimento_Caixa.OutrasTarifasDeAlteracao:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.OutrasTarifasDeAlteracao;
                        this.Descricao = "Outras tarifas de alteração";
                        break;
                    case EnumCodigoMovimento_Caixa.TarifasDiversas:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.TarifasDiversas;
                        this.Descricao = "Tarifas Diversas";
                        break;
                    case EnumCodigoMovimento_Caixa.LiquidacaoOnline:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.LiquidacaoOnline;
                        this.Descricao = "Liquidação On-line";
                        break;
                    case EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaSimples:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaSimples;
                        this.Descricao = "Transferência para a cobrança simples";
                        break;
                    case EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaDescontada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaDescontada;
                        this.Descricao = "Transferência para a cobrança descontada";
                        break;
                    case EnumCodigoMovimento_Caixa.ReconhecidoPeloPagadorDDA:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.ReconhecidoPeloPagadorDDA;
                        this.Descricao = "Reconhecido pelo pagador DDA";
                        break;
                    case EnumCodigoMovimento_Caixa.NaoReconhecidoPeloPagadorDDA:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.NaoReconhecidoPeloPagadorDDA;
                        this.Descricao = "Não reconhecido pelo pagador DDA";
                        break;
                    case EnumCodigoMovimento_Caixa.RecusadoNoDDA:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.RecusadoNoDDA;
                        this.Descricao = "Recusado no DDA";
                        break;
                    case EnumCodigoMovimento_Caixa.RejeicaoDoTitulo:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa.RejeicaoDoTitulo;
                        this.Descricao = "Rejeição do título";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }


        private void carregarLegado(int codigo)
        {
            try
            {
                this.Banco = new Banco_Caixa();

                switch ((EnumCodigoMovimento_Caixa_Legado)codigo)
                {
                    case EnumCodigoMovimento_Caixa_Legado.EntradaConfirmada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.EntradaConfirmada;
                        this.Descricao = "Entrada confirmada";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.EntradaRejeitada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.EntradaRejeitada;
                        this.Descricao = "Entrada rejeitada";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraEntrada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraEntrada;
                        this.Descricao = "Transferência de carteira/entrada";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraBaixa:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraBaixa;
                        this.Descricao = "Transferência de carteira/baixa";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.Liquidacao:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.Liquidacao;
                        this.Descricao = "Liquidação normal";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.Baixa:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.Baixa;
                        this.Descricao = "Baixa";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.TitulosCarteiraEmSer:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TitulosCarteiraEmSer;
                        this.Descricao = "Títulos em carteira em ser";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAbatimento:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAbatimento;
                        this.Descricao = "Confirmação recebimento instrução de abatimento";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento;
                        this.Descricao = "Confirmação recebimento instrução de cancelamento de abatimento";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento;
                        this.Descricao = "Confirmação recebimento instrução alteração de vencimento";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.FrancoPagamento:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.FrancoPagamento;
                        this.Descricao = "Franco pagamento";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.LiquidacaoAposBaixa:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.LiquidacaoAposBaixa;
                        this.Descricao = "Liquidação após baixa";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoProtesto;
                        this.Descricao = "Confirmação de recebimento de instrução de protesto";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoSustacaoProtesto:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoSustacaoProtesto;
                        this.Descricao = "Confirmação de recebimento de instrução de sustação de protesto";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.RemessaCartorio:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.RemessaCartorio;
                        this.Descricao = "Remessa a cartório/aponte em cartório";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.RetiradaCartorioManutencaoCarteira:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.RetiradaCartorioManutencaoCarteira;
                        this.Descricao = "Retirada de cartório e manutenção em carteira";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ProtestadoBaixado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ProtestadoBaixado;
                        this.Descricao = "Protestado e baixado/baixa por ter sido protestado";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.InstrucaoRejeitada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.InstrucaoRejeitada;
                        this.Descricao = "Instrução rejeitada";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.ConfirmaçãoPedidoAlteracaoOutrosDados:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmaçãoPedidoAlteracaoOutrosDados;
                        this.Descricao = "Confirmação do pedido de alteração de outros dados";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.DebitoTarifas:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.DebitoTarifas;
                        this.Descricao = "Debito de tarifas/custas";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.OcorrenciaSacado:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.OcorrenciaSacado;
                        this.Descricao = "Ocorrencias do sacado";
                        break;
                    case EnumCodigoMovimento_Caixa_Legado.AlteracaoDadosRejeitada:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.AlteracaoDadosRejeitada;
                        this.Descricao = "Alteração de dados rejeitada";
                        break;
                    default:
                        this.Codigo = 0;
                        this.Descricao = "( Selecione )";
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        private void Ler(int codigo)
        {
            try
            {
                switch (codigo)
                {
                    case 2:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.EntradaConfirmada;
                        this.Descricao = "Entrada confirmada";
                        break;
                    case 3:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.EntradaRejeitada;
                        this.Descricao = "Entrada rejeitada";
                        break;
                    case 4:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraEntrada;
                        this.Descricao = "Transferência de carteira/entrada";
                        break;
                    case 5:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraBaixa;
                        this.Descricao = "Transferência de carteira/baixa";
                        break;
                    case 6:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.Liquidacao;
                        this.Descricao = "Liquidação";
                        break;
                    case 9:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.Baixa;
                        this.Descricao = "Baixa";
                        break;
                    case 11:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.TitulosCarteiraEmSer;
                        this.Descricao = "Títulos em carteira em ser";
                        break;
                    case 12:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAbatimento;
                        this.Descricao = "Confirmação recebimento instrução de abatimento";
                        break;
                    case 13:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento;
                        this.Descricao = "Confirmação recebimento instrução de cancelamento de abatimento";
                        break;
                    case 14:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento;
                        this.Descricao = "Confirmação recebimento instrução alteração de vencimento";
                        break;
                    case 15:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.FrancoPagamento;
                        this.Descricao = "Franco pagamento";
                        break;
                    case 17:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.LiquidacaoAposBaixa;
                        this.Descricao = "Liquidação após baixa";
                        break;
                    case 19:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoProtesto;
                        this.Descricao = "Confirmação de recebimento de instrução de protesto";
                        break;
                    case 20:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoSustacaoProtesto;
                        this.Descricao = "Confirmação de recebimento de instrução de sustação de protesto";
                        break;
                    case 23:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.RemessaCartorio;
                        this.Descricao = "Remessa a cartório/aponte em cartório";
                        break;
                    case 24:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.RetiradaCartorioManutencaoCarteira;
                        this.Descricao = "Retirada de cartório e manutenção em carteira";
                        break;
                    case 25:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ProtestadoBaixado;
                        this.Descricao = "Protestado e baixado/baixa por ter sido protestado";
                        break;
                    case 26:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.InstrucaoRejeitada;
                        this.Descricao = "Instrução rejeitada";
                        break;
                    case 27:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.ConfirmaçãoPedidoAlteracaoOutrosDados;
                        this.Descricao = "Confirmação do pedido de alteração de outros dados";
                        break;
                    case 28:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.DebitoTarifas;
                        this.Descricao = "Debito de tarifas/custas";
                        break;
                    case 29:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.OcorrenciaSacado;
                        this.Descricao = "Ocorrencias do sacado";
                        break;
                    case 30:
                        this.Codigo = (int)EnumCodigoMovimento_Caixa_Legado.AlteracaoDadosRejeitada;
                        this.Descricao = "Alteração de dados rejeitada";
                        break;
                    default:
                        this.Codigo = 0;
                        this.Descricao = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }


        #endregion

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            if (this.legado)
                return ObterCorrespondenteFebraban(correspondentesFebraban_legado, (EnumCodigoMovimento_Caixa_Legado)Codigo);
            else
                return ObterCorrespondenteFebraban(correspondentesFebraban, (EnumCodigoMovimento_Caixa)Codigo);
        }

        private Dictionary<EnumCodigoMovimento_Caixa, TipoOcorrenciaRetorno> correspondentesFebraban = new Dictionary<EnumCodigoMovimento_Caixa, TipoOcorrenciaRetorno>()
        {
            { EnumCodigoMovimento_Caixa.EntradaConfirmada                     ,TipoOcorrenciaRetorno.EntradaConfirmada                                      },
            { EnumCodigoMovimento_Caixa.BaixaManualConfirmada                 ,TipoOcorrenciaRetorno.Baixa                                                  },
            { EnumCodigoMovimento_Caixa.AbatimentoConcedido                   ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeAbatimento            },
            { EnumCodigoMovimento_Caixa.AbatimentoCancelado                   ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeCancelamentoAbatimento},
            { EnumCodigoMovimento_Caixa.VencimentoAlterado                    ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoAlteracaoDeVencimento         },
            { EnumCodigoMovimento_Caixa.UsoEmpresaAlterado                    ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.PrazoProtestoAlterado                 ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.PrazoDevolucaoAlterado                ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.AlteracaoConfirmada                   ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.AlteracaoReemissaoBoletoConfirmada    ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.AlteracaoProtestoPDevolucaoConfirmada ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.AlteracaoDevolucaoPProtestoConfirmada ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.EmSer                                 ,TipoOcorrenciaRetorno.TitulosEmCarteira},
            { EnumCodigoMovimento_Caixa.Liquidacao                            ,TipoOcorrenciaRetorno.Liquidacao},
            { EnumCodigoMovimento_Caixa.LiquidacaoEmCartorio                  ,TipoOcorrenciaRetorno.Liquidacao},
            { EnumCodigoMovimento_Caixa.BaixaPorDevolucao                     ,TipoOcorrenciaRetorno.Baixa},
            { EnumCodigoMovimento_Caixa.BaixaPorProtesto                      ,TipoOcorrenciaRetorno.Baixa},
            { EnumCodigoMovimento_Caixa.TituloEnviadoCartorio                 ,TipoOcorrenciaRetorno.RemessaACartorio},
            { EnumCodigoMovimento_Caixa.SustacaoProtesto                      ,TipoOcorrenciaRetorno.TituloSustadoJudicialmente},
            { EnumCodigoMovimento_Caixa.EstornoProtesto                       ,TipoOcorrenciaRetorno.InstrucaoParacancelarProtestoConfirmada},
            { EnumCodigoMovimento_Caixa.EstornoSustacaoProtesto               ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeSustacaoCancelamentoDeProtesto},
            { EnumCodigoMovimento_Caixa.AlteracaoDoTitulo                     ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
            { EnumCodigoMovimento_Caixa.TarifaSobreTituloVencido              ,TipoOcorrenciaRetorno.DebitoDeTarifasCustas},
            { EnumCodigoMovimento_Caixa.OutrasTarifasDeAlteracao              ,TipoOcorrenciaRetorno.DebitoDeTarifasCustas},
            { EnumCodigoMovimento_Caixa.TarifasDiversas                       ,TipoOcorrenciaRetorno.DebitoDeTarifasCustas},
            { EnumCodigoMovimento_Caixa.LiquidacaoOnline                      ,TipoOcorrenciaRetorno.Liquidacao},
            { EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaSimples       ,TipoOcorrenciaRetorno.AlteracaoDecontratoDecobranca},
            { EnumCodigoMovimento_Caixa.TranferenciaParaCobrancaDescontada    ,TipoOcorrenciaRetorno.AlteracaoDecontratoDecobranca},
            { EnumCodigoMovimento_Caixa.ReconhecidoPeloPagadorDDA             ,TipoOcorrenciaRetorno.TituloDDAreconhecidoPeloPagador},
            { EnumCodigoMovimento_Caixa.NaoReconhecidoPeloPagadorDDA          ,TipoOcorrenciaRetorno.TituloDDANaoReconhecidoPeloPagador},
            { EnumCodigoMovimento_Caixa.RecusadoNoDDA                         ,TipoOcorrenciaRetorno.TituloDDArecusadoPelaCIP},
            { EnumCodigoMovimento_Caixa.RejeicaoDoTitulo                      ,TipoOcorrenciaRetorno.EntradaRejeitada},
        };

        private Dictionary<EnumCodigoMovimento_Caixa_Legado, TipoOcorrenciaRetorno> correspondentesFebraban_legado = new Dictionary<EnumCodigoMovimento_Caixa_Legado, TipoOcorrenciaRetorno>()
        {
            { EnumCodigoMovimento_Caixa_Legado.EntradaConfirmada                                          ,TipoOcorrenciaRetorno.EntradaConfirmada                                    },
            { EnumCodigoMovimento_Caixa_Legado.EntradaRejeitada                                           ,TipoOcorrenciaRetorno.EntradaRejeitada                                     },
            { EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraEntrada                               ,TipoOcorrenciaRetorno.TransferenciaDeCarteiraEntrada                         },
            { EnumCodigoMovimento_Caixa_Legado.TransferenciaCarteiraBaixa                                 ,TipoOcorrenciaRetorno.TransferenciaDeCarteiraBaixa                           },
            { EnumCodigoMovimento_Caixa_Legado.Liquidacao                                                 ,TipoOcorrenciaRetorno.Liquidacao                                           },
            { EnumCodigoMovimento_Caixa_Legado.Baixa                                                      ,TipoOcorrenciaRetorno.Baixa                                                },
            { EnumCodigoMovimento_Caixa_Legado.TitulosCarteiraEmSer                                       ,TipoOcorrenciaRetorno.TitulosEmCarteira                                 },
            { EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAbatimento                  ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeAbatimento            },
            { EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento      ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeCancelamentoAbatimento},
            { EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento         ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoAlteracaoDeVencimento   },
            { EnumCodigoMovimento_Caixa_Legado.FrancoPagamento                                            ,TipoOcorrenciaRetorno.FrancoDePagamento                                      },
            { EnumCodigoMovimento_Caixa_Legado.LiquidacaoAposBaixa                                        ,TipoOcorrenciaRetorno.LiquidacaoAposBaixaOuLiquidacaoTituloNaoRegistrado                                  },
            { EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoProtesto                    ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeProtesto              },
            { EnumCodigoMovimento_Caixa_Legado.ConfirmacaoRecebimentoInstrucaoSustacaoProtesto            ,TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeSustacaoCancelamentoDeProtesto      },
            { EnumCodigoMovimento_Caixa_Legado.RemessaCartorio                                            ,TipoOcorrenciaRetorno.RemessaACartorio                                      },
            { EnumCodigoMovimento_Caixa_Legado.RetiradaCartorioManutencaoCarteira                         ,TipoOcorrenciaRetorno.RetiradaDeCartorioEManutencaoEmCarteira                   },
            { EnumCodigoMovimento_Caixa_Legado.ProtestadoBaixado                                          ,TipoOcorrenciaRetorno.ProtestadoEBaixado                                    },
            { EnumCodigoMovimento_Caixa_Legado.InstrucaoRejeitada                                         ,TipoOcorrenciaRetorno.InstrucaoRejeitada                                   },
            { EnumCodigoMovimento_Caixa_Legado.ConfirmaçãoPedidoAlteracaoOutrosDados                      ,TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados                },
            { EnumCodigoMovimento_Caixa_Legado.DebitoTarifas                                              ,TipoOcorrenciaRetorno.DebitoDeTarifasCustas                                        },
            { EnumCodigoMovimento_Caixa_Legado.OcorrenciaSacado                                           ,TipoOcorrenciaRetorno.OcorrenciasDoPagador},
            { EnumCodigoMovimento_Caixa_Legado.AlteracaoDadosRejeitada                                    ,TipoOcorrenciaRetorno.AlteracaoDeDadosRejeitada }
        };
    }
}
