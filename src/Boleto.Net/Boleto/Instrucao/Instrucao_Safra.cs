using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumInstrucoes_Safra1
    {
        NaoReceberPrincipal = 1,
        DevolverAteQuinzeDias = 2,
        DevolverAteTrintaDias = 3,
        DevolverAPedido = 4,
        NaoCobrarJurosMora = 8,
        CobrarJurosMora = 16
    }

    #endregion

    public class Instrucao_Safra : AbstractInstrucao, IInstrucao
    {

        private int _NumeroInstrucao;
        public int NumeroInstrucao { get { return this._NumeroInstrucao; } set { this._NumeroInstrucao = value; } }

    #region Construtores

    public Instrucao_Safra()
        {
            try
            {
                this.Banco = new Banco(422);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar objeto", e);
            }
        }

        public Instrucao_Safra(int codigo, int nrDias)
        {
            try
            {
                this.Banco = new Banco_Safra();
                this.ExecInstrucao1(codigo, nrDias);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar objeto", e);
            }
        }
        #endregion Construtores


        private void ExecInstrucao1(int codigo, int nrDias)
        {
            switch ((EnumInstrucoes_Safra1)codigo)
            {
                case EnumInstrucoes_Safra1.NaoReceberPrincipal:
                    this.Codigo = (int)EnumInstrucoes_Safra1.NaoReceberPrincipal;
                    this.Descricao = "NÃO RECEBER PRINCIPAL, SEM JUROS DE MORA";
                    break;
                case EnumInstrucoes_Safra1.DevolverAteQuinzeDias:
                    this.Codigo = (int)EnumInstrucoes_Safra1.DevolverAteQuinzeDias;
                    this.Descricao = "DEVOLVER, SE NÃO PAGO, ATÉ 15 DIAS APÓS O VENCIMENTO";
                    break;
                case EnumInstrucoes_Safra1.DevolverAteTrintaDias:
                    this.Codigo = (int)EnumInstrucoes_Safra1.DevolverAteTrintaDias;
                    this.Descricao = "DEVOLVER, SE NÃO PAGO, ATÉ 30 DIAS APÓS O VENCIMENTO";
                    break;
                case EnumInstrucoes_Safra1.DevolverAPedido:
                    this.Codigo = (int)EnumInstrucoes_Safra1.DevolverAPedido;
                    this.Descricao = "DEVOLVER A PEDIDO";
                    break;
                case EnumInstrucoes_Safra1.NaoCobrarJurosMora:
                    this.Codigo = (int)EnumInstrucoes_Safra1.NaoCobrarJurosMora;
                    this.Descricao = "NÃO COBRAR JUROS DE MORA";
                    break;
                case EnumInstrucoes_Safra1.CobrarJurosMora:
                    this.Codigo = (int)EnumInstrucoes_Safra1.CobrarJurosMora;
                    this.Descricao = "COBRAR JUROS DE MORA";
                    break;
            }
            
            this.QuantidadeDias = nrDias;
        }
    }
}
