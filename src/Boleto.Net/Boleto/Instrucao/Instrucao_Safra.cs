using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumInstrucoes_Safra1
    {
        /*
         * 01 - NÃO RECEBER PRINCIPAL, SEM JUROS DE MORA
         * 02 - DEVOLVER, SE NÃO PAGO, ATÉ 15 DIAS APÓS O VENCIMENTO
         * 03 - DEVOLVER, SE NÃO PAGO, ATÉ 30 DIAS APÓS O VENCIMENTO
         * 07 - NÃO PROTESTAR
         * 08 - NÃO COBRAR JUROS DE MORA
         * 16 - MULTA(*)
            (*) Para tratamento de multa, formatar no campo “abatimento” (pos. 206 a 218), as
            seguintes informações:
            Posição 206 a 211 a data a partir da qual a multa deve ser cobrada (ddmmaa)
            Posição 212 a 215 o percentual referente à multa no formato 99v99.
            Posição 216 a 218 zeros
         *
         */

        NaoReceberPrincipal = 1,
        DevolverAteQuinzeDias,
        DevolverAteTrintaDias,
        NaoProtestar = 7,
        NaoCobrarJurosMora,
        Multa = 16
    }

    public enum EnumInstrucoes_Safra2
    {
        /* 
         * 01 - Cobrar Juros de Mora (*)
         * 10 - PROTESTO AUTOMÁTICO
         * 
         */

        CobrarJurosMora = 1,
        ProtestoAutomatico = 10
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

        public Instrucao_Safra(int codigo, int nrDias, int numeroInstrucao)
        {
            try
            {
                this.Banco = new Banco_Safra();

                if(numeroInstrucao == 1)
                {
                    this.ExecInstrucao1(codigo, nrDias);
                }
                else if(numeroInstrucao == 2) {
                    this.ExecInstrucao2(codigo, nrDias);
                } 
                else
                {
                    throw new Exception("Instrução: " + numeroInstrucao + " inexistente ou não implementada.");
                }
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
                case EnumInstrucoes_Safra1.NaoProtestar:
                    this.Codigo = (int)EnumInstrucoes_Safra1.NaoProtestar;
                    this.Descricao = "NÃO PROTESTAR";
                    break;
                case EnumInstrucoes_Safra1.NaoCobrarJurosMora:
                    this.Codigo = (int)EnumInstrucoes_Safra1.NaoCobrarJurosMora;
                    this.Descricao = "NÃO COBRAR JUROS DE MORA";
                    break;
                case EnumInstrucoes_Safra1.Multa:
                    this.Codigo = (int)EnumInstrucoes_Safra1.Multa;
                    this.Descricao = "MULTA";
                    break;
            }
            
            this.QuantidadeDias = nrDias;
        }

        private void ExecInstrucao2(int codigo, int nrDias)
        {
            switch ((EnumInstrucoes_Safra2)codigo)
            {
                case EnumInstrucoes_Safra2.CobrarJurosMora:
                    this.Codigo = (int)EnumInstrucoes_Safra2.CobrarJurosMora;
                    this.Descricao = "COBRAR JUROS DE MORA";
                    break;
                case EnumInstrucoes_Safra2.ProtestoAutomatico:
                    this.Codigo = (int)EnumInstrucoes_Safra2.ProtestoAutomatico;
                    this.Descricao = "PROTESTO AUTOMÁTICO";
                    break;
            }

            this.QuantidadeDias = nrDias;
        }
    }
}
