using System;
using System.Collections;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumInstrucoes_Bradesco
    {
        NaoCobrarJurosDeMora = 1,
        NaoReceberAposOVencimento = 2,
        MultasDe10AposO4DiaDoVencimento = 3,
        NaoReceberposo8DiaDoVencimento = 4,
        CobrarEncargosAposO5DiaDoVencimento = 5,
        CobrarEncargosAposO10DiaDoVencimento = 6,
        CobrarEncargosAposO15DiaDoVencimento = 7,
        ConcederDescontoMesmoSePagoAposOVencimento = 8,

        Protestar = 9,
        NaoProtestar = 10,
        ProtestoFinsFalimentares = 42,
        ProtestarAposNDiasCorridos = 81,
        ProtestarAposNDiasUteis = 82,
        NaoReceberAposNDias = 91,
        DevolverAposNDias = 92
    }

    #endregion 

    public class Instrucao_Bradesco : AbstractInstrucao, IInstrucao
    {

        #region Construtores 

		public Instrucao_Bradesco()
		{
			try
			{
                this.Banco = new Banco(237);
			}
			catch (Exception ex)
			{
                throw new Exception("Erro ao carregar objeto", ex);
			}
		}

        public Instrucao_Bradesco(int codigo)
        {
            this.carregar(codigo, 0);
        }

        public Instrucao_Bradesco(int codigo, int nrDias)
        {
            this.carregar(codigo, nrDias);
        }
        //public Instrucao_Bradesco(int codigo, double valor)
        //{
        //    this.carregar(codigo, valor);
        //}

        //public Instrucao_Bradesco(int codigo, double valor, EnumTipoValor tipoValor)
        //{
        //    this.carregar(codigo, valor, tipoValor);
        //}

        //public Instrucao_Bradesco(int codigo, double valor, DateTime data, EnumTipoValor tipoValor)
        //{
        //    this.carregar(codigo, valor, data, tipoValor);
        //}

        #endregion Construtores

        #region Metodos Privados

        //private void carregar(int idInstrucao, double valor, EnumTipoValor tipoValor = EnumTipoValor.Percentual)
        //{
        //    try
        //    {
        //        this.Banco = new Banco_Bradesco();
        //        this.Valida();

        //        switch ((EnumInstrucoes_Bradesco)idInstrucao)
        //        {
        //            case EnumInstrucoes_Bradesco.OutrasInstrucoes_ExibeMensagem_MoraDiaria:
        //                this.Codigo = 0;
        //                this.Descricao = String.Format("Após vencimento cobrar juros de {0} {1} por dia de atraso",
        //                    (tipoValor.Equals(EnumTipoValor.Reais) ? "R$ " : valor.ToString("F2")),
        //                    (tipoValor.Equals(EnumTipoValor.Percentual) ? "%" : valor.ToString("F2")));
        //                break;
        //            case EnumInstrucoes_Bradesco.OutrasInstrucoes_ExibeMensagem_MultaVencimento:
        //                this.Codigo = 0;
        //                this.Descricao = String.Format("Após vencimento cobrar multa de {0} {1}",
        //                    (tipoValor.Equals(EnumTipoValor.Reais) ? "R$ " : valor.ToString("F2")),
        //                    (tipoValor.Equals(EnumTipoValor.Percentual) ? "%" : valor.ToString("F2")));
        //                break;
        //            default:
        //                this.Codigo = 0;
        //                this.Descricao = " (Selecione) ";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro ao carregar objeto", ex);
        //    }
        //}

        private void carregar(int idInstrucao, int nrDias)
        {
            try
            {
                this.Banco = new Banco_Bradesco();
                this.Valida();

                switch ((EnumInstrucoes_Bradesco)idInstrucao)
                {
                    case EnumInstrucoes_Bradesco.Protestar:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Protestar";
                        break;
                    case EnumInstrucoes_Bradesco.NaoProtestar:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.NaoProtestar;
                        this.Descricao = "Não protestar";
                        break;
                    case EnumInstrucoes_Bradesco.ProtestoFinsFalimentares:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.ProtestoFinsFalimentares;
                        this.Descricao = "Protesto para fins falimentares";
                        break;
                    case EnumInstrucoes_Bradesco.ProtestarAposNDiasCorridos:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.ProtestarAposNDiasCorridos;
                        this.Descricao = "Protestar após " + nrDias + " dias corridos do vencimento";
                        break;
                    case EnumInstrucoes_Bradesco.ProtestarAposNDiasUteis:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.ProtestarAposNDiasUteis;
                        this.Descricao = "Protestar após " + nrDias + " dias úteis do vencimento";
                        break;
                    case EnumInstrucoes_Bradesco.NaoReceberAposNDias:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.NaoReceberAposNDias;
                        this.Descricao = "Não receber após " + nrDias + " dias do vencimento";
                        break;
                    case EnumInstrucoes_Bradesco.DevolverAposNDias:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.DevolverAposNDias;
                        this.Descricao = "Devolver após " + nrDias + " dias do vencimento";
                        break;


                    case EnumInstrucoes_Bradesco.NaoCobrarJurosDeMora:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.NaoCobrarJurosDeMora;
                        this.Descricao = "Não cobrar juros de mora";
                        break;

                    case EnumInstrucoes_Bradesco.NaoReceberAposOVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.NaoReceberAposOVencimento;
                        this.Descricao = "Não receber após o vencimento";
                        break;

                    case EnumInstrucoes_Bradesco.MultasDe10AposO4DiaDoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.MultasDe10AposO4DiaDoVencimento;
                        this.Descricao = "Multas de 10% após o 4º dia do Vencimento.";
                        break;

                    case EnumInstrucoes_Bradesco.NaoReceberposo8DiaDoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.NaoReceberposo8DiaDoVencimento;
                        this.Descricao = "Não receberpóso 8º dia do vencimento.";
                        break;

                    case EnumInstrucoes_Bradesco.CobrarEncargosAposO5DiaDoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.CobrarEncargosAposO5DiaDoVencimento;
                        this.Descricao = "Cobrar encargos após o 5º dia do vencimento.";
                        break;

                    case EnumInstrucoes_Bradesco.CobrarEncargosAposO10DiaDoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.CobrarEncargosAposO10DiaDoVencimento;
                        this.Descricao = "Cobrar encargos após o 10º dia do vencimento.";
                        break;

                    case EnumInstrucoes_Bradesco.CobrarEncargosAposO15DiaDoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.CobrarEncargosAposO15DiaDoVencimento;
                        this.Descricao = "Cobrar encargos após o 15º dia do vencimento";
                        break;

                    case EnumInstrucoes_Bradesco.ConcederDescontoMesmoSePagoAposOVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.ConcederDescontoMesmoSePagoAposOVencimento;
                        this.Descricao = "Conceder desconto mesmo se pago após o vencimento.";
                        break;
                }

                this.QuantidadeDias = nrDias;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        //private void carregar(int idInstrucao, double valor, DateTime data, EnumTipoValor tipoValor = EnumTipoValor.Reais)
        //{
        //    try
        //    {
        //        this.Banco = new Banco_Bradesco();
        //        this.Valida();

        //        switch ((EnumInstrucoes_Bradesco)idInstrucao)
        //        {
        //            case EnumInstrucoes_Bradesco.ComDesconto:
        //                this.Codigo = (int)EnumInstrucoes_Bradesco.ComDesconto;
        //                this.Descricao = String.Format("Desconto de pontualidade no valor de {0} {1} se pago até " + data.ToShortDateString(),
        //                    (tipoValor.Equals(EnumTipoValor.Reais) ? "R$ " : valor.ToString("C")),
        //                    (tipoValor.Equals(EnumTipoValor.Percentual) ? "%" : valor.ToString("F2")));
        //                break;
        //            case EnumInstrucoes_Bradesco.BoletoOriginal:
        //                this.Codigo = (int)EnumInstrucoes_Bradesco.BoletoOriginal;
        //                this.Descricao = "Vencimento " + data.ToShortDateString() + ", no valor de " + valor.ToString("C") + "";
        //                break;
        //            default:
        //                this.Codigo = 0;
        //                this.Descricao = " (Selecione) ";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro ao carregar objeto", ex);
        //    }
        //}


        public override void Valida()
        {
            //base.Valida();
        }

        #endregion

    }
}
