using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet.EDI.Banco
{
    class TRegistroEDI_Unicred_Retorno : TRegistroEDI
    {

        #region Atributos e Propriedades

        #region atributos
        private string _Identificacao = String.Empty;
        private string _InscricaoEmpresa;
        private string _NumeroInscricao;
        private string _Agencia;
        private string _DigitoAgencia;
        private string _Conta;
        private string _DigitoConta;
        private string _Beneficiario;
        private string _NossoNumero;
        private string _Brancos1;
        private string _Zeros1;
        private string _Fixo0;
        private string _Brancos2;
        private string _Fixo1;
        private string _Zeros2;
        private string _Fixo2;
        private string _TipoOcorrencia;
        private string _DataLiquidacao;
        private string _Brancos3;
        private string _VencimentoTitulo;
        private string _ValorNominalTitulo;
        private string _CodigoBanco;
        private string _AgenciaEncarregada;
        private string _DVAgenciaEncarregada;
        private string _Brancos4;
        private string _DataRepasse;
        private string _ValorTarifas;
        private string _Brancos5;
        private string _ValorAbatimento;
        private string _ValorDescontoConcedido;
        private string _ValorPago;
        private string _ValorJurosMora;
        private string _SeuNumero;
        private string _ValorLiquido;
        private string _ComplementoMovimento;
        private string _InstrucaoOrigem;
        private string _Brancos6;
        private string _SequencialRegistro;

        
        #endregion

        #region propriedades
        public string Identificacao { get => _Identificacao; set => _Identificacao = value; }
        public string InscricaoEmpresa { get => _InscricaoEmpresa; set => _InscricaoEmpresa = value; }
        public string NumeroInscricao { get => _NumeroInscricao; set => _NumeroInscricao = value; }
        public string Agencia { get => _Agencia; set => _Agencia = value; }
        public string DigitoAgencia { get => _DigitoAgencia; set => _DigitoAgencia = value; }
        public string Conta { get => _Conta; set => _Conta = value; }
        public string DigitoConta { get => _DigitoConta; set => _DigitoConta = value; }
        public string Beneficiario { get => _Beneficiario; set => _Beneficiario = value; }
        public string NossoNumero { get => _NossoNumero; set => _NossoNumero = value; }
        public string Brancos1 { get => _Brancos1; set => _Brancos1 = value; }
        public string Zeros1 { get => _Zeros1; set => _Zeros1 = value; }
        public string Fixo0 { get => _Fixo0; set => _Fixo0 = value; }
        public string Brancos2 { get => _Brancos2; set => _Brancos2 = value; }
        public string Fixo1 { get => _Fixo1; set => _Fixo1 = value; }
        public string Zeros2 { get => _Zeros2; set => _Zeros2 = value; }
        public string Fixo2 { get => _Fixo2; set => _Fixo2 = value; }
        public string TipoOcorrencia { get => _TipoOcorrencia; set => _TipoOcorrencia = value; }
        public string DataLiquidacao { get => _DataLiquidacao; set => _DataLiquidacao = value; }
        public string Brancos3 { get => _Brancos3; set => _Brancos3 = value; }
        public string VencimentoTitulo { get => _VencimentoTitulo; set => _VencimentoTitulo = value; }        
        public string ValorNominalTitulo { get => _ValorNominalTitulo; set => _ValorNominalTitulo = value; }
        public string CodigoBanco { get => _CodigoBanco; set => _CodigoBanco = value; }
        public string AgenciaEncarregada { get => _AgenciaEncarregada; set => _AgenciaEncarregada = value; }
        public string DVAgenciaEncarregada { get => _DVAgenciaEncarregada; set => _DVAgenciaEncarregada = value; }
        public string Brancos4 { get => _Brancos4; set => _Brancos4 = value; }
        public string DataRepasse { get => _DataRepasse; set => _DataRepasse = value; }
        public string ValorTarifas { get => _ValorTarifas; set => _ValorTarifas = value; }
        public string Brancos5 { get => _Brancos5; set => _Brancos5 = value; }
        public string ValorAbatimento { get => _ValorAbatimento; set => _ValorAbatimento = value; }
        public string ValorDescontoConcedido { get => _ValorDescontoConcedido; set => _ValorDescontoConcedido = value; }
        public string ValorPago { get => _ValorPago; set => _ValorPago = value; }
        public string ValorJurosMora { get => _ValorJurosMora; set => _ValorJurosMora = value; }
        public string SeuNumero { get => _SeuNumero; set => _SeuNumero = value; }
        public string ValorLiquido { get => _ValorLiquido; set => _ValorLiquido = value; }
        public string ComplementoMovimento { get => _ComplementoMovimento; set => _ComplementoMovimento = value; }
        public string InstrucaoOrigem { get => _InstrucaoOrigem; set => _InstrucaoOrigem = value; }
        public string Brancos6 { get => _Brancos6; set => _Brancos6 = value; }
        public string SequencialRegistro { get => _SequencialRegistro; set => _SequencialRegistro = value; }
        #endregion
        #endregion


        public TRegistroEDI_Unicred_Retorno()
        {
            /*
             * Aqui é que iremos informar as características de cada campo do arquivo
             * Na classe base, TCampoRegistroEDI, temos a propriedade CamposEDI, que é uma coleção de objetos
             * TCampoRegistroEDI.
             */
            #region TODOS os Campos
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0001, 001, 0, string.Empty, ' ')); //001-001
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 002, 0, string.Empty, ' ')); //002-003
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0004, 014, 0, string.Empty, ' ')); //004-017
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0018, 004, 0, string.Empty, ' ')); //018-021
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 001, 0, string.Empty, ' ')); //022-022
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0023, 008, 0, string.Empty, ' ')); //023-030
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, string.Empty, ' ')); //031-031
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 014, 0, string.Empty, ' ')); //032-037
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0046, 017, 0, string.Empty, ' ')); //038-062
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 011, 0, string.Empty, ' ')); //063-071
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0074, 001, 0, string.Empty, ' ')); //072-101
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0075, 001, 0, string.Empty, ' ')); //102-102
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0076, 010, 0, string.Empty, ' ')); //103-104
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0086, 003, 0, string.Empty, ' ')); //105-105
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0089, 018, 0, string.Empty, ' ')); //106-107
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0107, 002, 0, string.Empty, ' ')); //108-108
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, string.Empty, ' ')); //109-110
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 006, 0, string.Empty, ' ')); //111-120
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0117, 030, 0, string.Empty, ' ')); //121-126
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0147, 006, 0, string.Empty, ' ')); //127-139
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0153, 013, 0, string.Empty, ' ')); //140-142
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0166, 003, 0, string.Empty, ' ')); //143-147
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0169, 004, 0, string.Empty, ' ')); //148-149
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0173, 001, 0, string.Empty, ' ')); //150-150
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0174, 002, 0, string.Empty, ' ')); //151-156
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0176, 006, 0, string.Empty, ' ')); //157-158
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0182, 007, 0, string.Empty, ' ')); //159-160
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0189, 039, 0, string.Empty, ' ')); //161-173
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0228, 013, 0, string.Empty, ' ')); //174-179
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0241, 013, 0, string.Empty, ' ')); //180-192
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0254, 013, 0, string.Empty, ' ')); //193-205
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0267, 013, 0, string.Empty, ' ')); //206-218
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0280, 026, 0, string.Empty, ' ')); //219-220
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0306, 013, 0, string.Empty, ' ')); //221-234
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0319, 008, 0, string.Empty, ' ')); //235-274
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0327, 002, 0, string.Empty, ' ')); //275-314 
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0329, 066, 0, string.Empty, ' ')); //315-324
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0395, 006, 0, string.Empty, ' ')); //325-326
            #endregion
        }

        public override void CodificarLinha()
        {
            #region Todos os Campos
            //PARA LEITURA DO RETORNO BANCÁRIO NÃO PRECISAMOS IMPLEMENTAR ESSE MÉTODO           
            #endregion
            //
            base.CodificarLinha(); //Aqui que eu chamo efetivamente a rotina de codificação; o resultado será exibido na propriedade LinhaRegistro.
        }

        public override void DecodificarLinha()
        {
            base.DecodificarLinha();

            this.Identificacao = (string)this._CamposEDI[0].ValorNatural;
            this.InscricaoEmpresa = (string)this.CamposEDI[1].ValorNatural;
            this.NumeroInscricao = (string)this.CamposEDI[2].ValorNatural;
            this.Agencia = (string)this.CamposEDI[3].ValorNatural;
            this.DigitoAgencia = (string)this.CamposEDI[4].ValorNatural;
            this.Conta = (string)this.CamposEDI[5].ValorNatural;
            this.DigitoConta = (string)this.CamposEDI[6].ValorNatural;
            this.Beneficiario = (string)this.CamposEDI[7].ValorNatural;
            this.NossoNumero = (string)this.CamposEDI[8].ValorNatural;
            this.Brancos1 = (string)this.CamposEDI[9].ValorNatural;
            this.Zeros1 = (string)this.CamposEDI[10].ValorNatural;
            this.Fixo0 = (string)this.CamposEDI[11].ValorNatural; 
            this.Brancos2 = (string)this.CamposEDI[12].ValorNatural;
            this.Fixo1 = (string)this.CamposEDI[13].ValorNatural;
            this.Zeros2 = (string)this.CamposEDI[14].ValorNatural;
            this.Fixo2 = (string)this.CamposEDI[15].ValorNatural;
            this.TipoOcorrencia = (string)this.CamposEDI[16].ValorNatural;
            this.DataLiquidacao = (string)this.CamposEDI[17].ValorNatural;
            this.Brancos3 = (string)this.CamposEDI[18].ValorNatural;
            this.VencimentoTitulo = (string)this.CamposEDI[19].ValorNatural;
            this.ValorNominalTitulo = (string)this.CamposEDI[20].ValorNatural;
            this.CodigoBanco = (string)this.CamposEDI[21].ValorNatural;
            this.AgenciaEncarregada = (string)this.CamposEDI[22].ValorNatural;
            this.DVAgenciaEncarregada = (string)this.CamposEDI[23].ValorNatural;
            this.Brancos4 = (string)this.CamposEDI[24].ValorNatural;
            this.DataRepasse = (string)this.CamposEDI[25].ValorNatural;
            this.ValorTarifas = (string)this.CamposEDI[26].ValorNatural;
            this.Brancos5 = (string)this.CamposEDI[27].ValorNatural;
            this.ValorAbatimento = (string)this.CamposEDI[28].ValorNatural;
            this.ValorDescontoConcedido = (string)this.CamposEDI[29].ValorNatural;
            this.ValorPago = (string)this.CamposEDI[30].ValorNatural;
            this.ValorJurosMora = (string)this.CamposEDI[31].ValorNatural;
            this.SeuNumero = (string)this.CamposEDI[32].ValorNatural;
            this.ValorLiquido = (string)this.CamposEDI[33].ValorNatural;
            this.ComplementoMovimento = (string)this.CamposEDI[34].ValorNatural;
            this.InstrucaoOrigem = (string)this.CamposEDI[35].ValorNatural;
            this.Brancos6 = (string)this.CamposEDI[36].ValorNatural;
            this.SequencialRegistro = (string)this.CamposEDI[37].ValorNatural;
        }
    }
}
