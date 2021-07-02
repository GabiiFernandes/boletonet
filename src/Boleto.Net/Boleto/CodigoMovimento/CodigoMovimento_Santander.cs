using BoletoNet.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumCodigoMovimento_Santander
    {
		EntradaConfirmada = 02,
		EntradaRejeitada = 03,
		TransferenciaCarteiraSimples = 04,
		TransferenciaCarteiraDesconto = 05,
		Liquidacao = 06,
		ConfirmacaoRecebimentoCancelamentoDesconto = 08,
		Baixa = 09,
		TituloEmCarteira = 11,
		ConfirmRecebInstrucaoAbatimento = 12,
		ConfirmRecebInstrucaoCancelAbatimento = 13,
		ConfirmRecebInstrucaoAltVencimento = 14,
		liquidacaoBaixaLiquidTituloNRegistrado = 17,
		ConfirmRecebInstrucaoProtesto = 19,
		ConfirmRecebInstrucaoSustar = 20,
		RemessaCartorio = 23,
		RetiradaCartorio = 24, 
		ProtestadoBaixado = 25,
		InstrucaoRejeitada = 26, 
		ConfirmPedidoAltOutrosDados = 27, 
		DebitoTarifas = 28,
		OcorrenciasPagador= 29,
		AlteracaoDadosRejeitada = 30,
		TituloDDAReconPagador = 51,
		TituloDDANReconPagador = 52,
		TituloDDARecusadoCIP = 53,
		AltValorNominalTitulo = 61
    }

    public class CodigoMovimento_Santander : AbstractCodigoMovimento, ICodigoMovimento
    {
        internal CodigoMovimento_Santander()
        {
        }

        public CodigoMovimento_Santander(int codigo)
        {
            try
            {
                Carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        private void Carregar(int codigo)
        {
            try
            {
                this.Banco = new Banco_Santander();

                var movimento = (EnumCodigoMovimento_Santander)codigo;
                Codigo = codigo;
                Descricao = descricoes[movimento];
            }
            catch (Exception ex)
            {
                throw new BoletoNetException("Código de movimento é inválido", ex);
            }
        }

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            return ObterCorrespondenteFebraban(correspondentesFebraban, (EnumCodigoMovimento_Santander)Codigo);
        }

        private readonly Dictionary<EnumCodigoMovimento_Santander, string> descricoes = new Dictionary<EnumCodigoMovimento_Santander, string>()
        {
            { EnumCodigoMovimento_Santander.EntradaConfirmada, "Entrada Confirmada" },
            { EnumCodigoMovimento_Santander.EntradaRejeitada, "Entrada Rejeitada" },
            { EnumCodigoMovimento_Santander.TransferenciaCarteiraSimples, " para carteira Simples" },
            { EnumCodigoMovimento_Santander.TransferenciaCarteiraDesconto, "Transferência para carteira com desconto/penhor/vendor/FIDC" },
            { EnumCodigoMovimento_Santander.Liquidacao, "Liquidação" },
            { EnumCodigoMovimento_Santander.ConfirmacaoRecebimentoCancelamentoDesconto, "Confirmação do Recebimento do Cancelamento do Desconto" },
            { EnumCodigoMovimento_Santander.Baixa, "Baixa" },
            { EnumCodigoMovimento_Santander.TituloEmCarteira, "Títulos em carteira (em ser)" },
            { EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoAbatimento, "Confirmação recebimento instrução de abatimento" },
            { EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoCancelAbatimento, "Confirmação recebimento instrução cancelamento abatimento" },
            { EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoAltVencimento, "Confirmção recebimento instrução alteração do vencimento" },
            { EnumCodigoMovimento_Santander.liquidacaoBaixaLiquidTituloNRegistrado, "Liquidação após baixa ou liquidação título não registrado" },
            { EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoProtesto, "Confirmação Recebimento instrução de protesto" },
            { EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoSustar, "Confirmação recebimento instrução de sustação/Não protestar" },
            { EnumCodigoMovimento_Santander.RemessaCartorio, "Remessa a cartorio (aponte em cartorio)" },
            { EnumCodigoMovimento_Santander.RetiradaCartorio, "Retirada de cartorio e manutenção em carteira" },
            { EnumCodigoMovimento_Santander.ProtestadoBaixado, "Prostestado e baixado (baixa por ter sido protestado)" },
            { EnumCodigoMovimento_Santander.InstrucaoRejeitada, "Instrução rejeitada" },
            { EnumCodigoMovimento_Santander.ConfirmPedidoAltOutrosDados, "Confirmação do pedido de alteração de outros dados" },
            { EnumCodigoMovimento_Santander.DebitoTarifas, "Debito de tarifas/custas" },
            { EnumCodigoMovimento_Santander.OcorrenciasPagador, "Ocorrências do pagador" },
            { EnumCodigoMovimento_Santander.AlteracaoDadosRejeitada, "Alteração de dados Rejeitada" },
            { EnumCodigoMovimento_Santander.TituloDDAReconPagador, "Título DDA reconhecido pelo Pagador" },
            { EnumCodigoMovimento_Santander.TituloDDANReconPagador, "Título DDA não reconhecido pelo Pagador" },
            { EnumCodigoMovimento_Santander.TituloDDARecusadoCIP, "Título DDA recusado pela CIP" },
            { EnumCodigoMovimento_Santander.AltValorNominalTitulo, "Confirmação de alteração do valor nominal do título" },
        };

        private readonly Dictionary<EnumCodigoMovimento_Santander, TipoOcorrenciaRetorno> correspondentesFebraban = new Dictionary<EnumCodigoMovimento_Santander, TipoOcorrenciaRetorno>()
        {
			{ EnumCodigoMovimento_Santander.EntradaConfirmada, TipoOcorrenciaRetorno.EntradaConfirmada },
			{ EnumCodigoMovimento_Santander.EntradaRejeitada, TipoOcorrenciaRetorno.EntradaRejeitada },
			{ EnumCodigoMovimento_Santander.Liquidacao, TipoOcorrenciaRetorno.Liquidacao},
			{ EnumCodigoMovimento_Santander.TransferenciaCarteiraSimples, TipoOcorrenciaRetorno.TransferenciaDeCarteiraEntrada},
			{ EnumCodigoMovimento_Santander.TransferenciaCarteiraDesconto, TipoOcorrenciaRetorno.TransferenciaDeCarteiraBaixa},
			{ EnumCodigoMovimento_Santander.ConfirmacaoRecebimentoCancelamentoDesconto, TipoOcorrenciaRetorno.ConfirmacaoDoRecebimentoDoCancelamentoDoDesconto},
			{ EnumCodigoMovimento_Santander.Baixa, TipoOcorrenciaRetorno.Baixa},
			{ EnumCodigoMovimento_Santander.TituloEmCarteira, TipoOcorrenciaRetorno.TitulosEmCarteira},
			{ EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoAbatimento, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeAbatimento},
			{ EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoCancelAbatimento, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeCancelamentoAbatimento},
			{ EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoAltVencimento, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoAlteracaoDeVencimento},
			{ EnumCodigoMovimento_Santander.liquidacaoBaixaLiquidTituloNRegistrado, TipoOcorrenciaRetorno.LiquidacaoAposBaixaOuLiquidacaoTituloNaoRegistrado},
			{ EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoProtesto, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeProtesto},
			{ EnumCodigoMovimento_Santander.ConfirmRecebInstrucaoSustar, TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeSustacaoCancelamentoDeProtesto},
			{ EnumCodigoMovimento_Santander.RemessaCartorio, TipoOcorrenciaRetorno.RemessaACartorio},
			{ EnumCodigoMovimento_Santander.RetiradaCartorio, TipoOcorrenciaRetorno.RetiradaDeCartorioEManutencaoEmCarteira},
			{ EnumCodigoMovimento_Santander.ProtestadoBaixado, TipoOcorrenciaRetorno.ProtestadoEBaixado},
			{ EnumCodigoMovimento_Santander.InstrucaoRejeitada, TipoOcorrenciaRetorno.InstrucaoRejeitada},
			{ EnumCodigoMovimento_Santander.ConfirmPedidoAltOutrosDados, TipoOcorrenciaRetorno.ConfirmacaoDoPedidoDeAlteracaoDeOutrosDados},
			{ EnumCodigoMovimento_Santander.DebitoTarifas, TipoOcorrenciaRetorno.DebitoDeTarifasCustas},
			{ EnumCodigoMovimento_Santander.OcorrenciasPagador, TipoOcorrenciaRetorno.OcorrenciasDoPagador},
			{ EnumCodigoMovimento_Santander.AlteracaoDadosRejeitada, TipoOcorrenciaRetorno.AlteracaoDeDadosRejeitada},
			{ EnumCodigoMovimento_Santander.TituloDDAReconPagador, TipoOcorrenciaRetorno.TituloDDAreconhecidoPeloPagador},
			{ EnumCodigoMovimento_Santander.TituloDDANReconPagador, TipoOcorrenciaRetorno.TituloDDANaoReconhecidoPeloPagador},
			{ EnumCodigoMovimento_Santander.TituloDDARecusadoCIP, TipoOcorrenciaRetorno.TituloDDArecusadoPelaCIP},
			{ EnumCodigoMovimento_Santander.AltValorNominalTitulo, TipoOcorrenciaRetorno.ConfirmacaoDeAlteracaoDoValorNominalDoTitulo},
        };
    }
}
