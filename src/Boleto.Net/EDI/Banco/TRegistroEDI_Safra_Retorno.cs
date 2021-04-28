using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet.EDI.Banco {
    class TRegistroEDI_Safra_Retorno : TRegistroEDI {

        #region Atributos e Propriedades

        #region atributos
        private string _Identificacao = String.Empty;
        private string _InscricaoEmpresa;
        private string _NumeroInscricao;
        private string _IdentEmpresa;
        private string _Brancos1;
        private string _Exclusivo;
        private string _IdentificacaoTitulo;
        private string _Brancos2;
        private string _TipoOcorrencia;
        private string _CodRejeicao;
        private string _TipoCarteira;
        private string _IdentOcorrencia;
        private string _DataOcorBanco;
        private string _TituloEmpresa;
        private string _NumTitulo;
        private string _Brancos3;
        private string _VencimentoTitulo;
        private string _ValorNominalTitulo;
        private string _CodigoBanco;
        private string _AgenciaEncarregada;
        private string _EspecieTitulo;
        private string _TarifaCobranca;
        private string _OutrasDespesas;
        private string _Zeros;
        private string _ValorIOF;
        private string _ValorAbatimentoConcedido;
        private string _ValorDescontoConcedido;
        private string _Valorliq;
        private string _ValorJurosMora;
        private string _OutrosCreditos;
        private string _IdentificacaoMoeda;
        private string _DataCredito;
        private string _Brancos4;
        private string _BeneficTransf;
        private string _TituloDDA;
        private string _MeioLiquidacao;
        private string _Brancos5;
        private string _SeuNumero;
        private string _SequencialArquivo;
        private string _SequencialRegistro;
        #endregion

        #region propriedades
        public string Identificacao { get => _Identificacao; set => _Identificacao = value; }
        public string InscricaoEmpresa { get => _InscricaoEmpresa; set => _InscricaoEmpresa = value; }
        public string NumeroInscricao { get => _NumeroInscricao; set => _NumeroInscricao = value; }
        public string IdentEmpresa { get => _IdentEmpresa; set => _IdentEmpresa = value; }
        public string Brancos1 { get => _Brancos1; set => _Brancos1 = value; }
        public string Exclusivo { get => _Exclusivo; set => _Exclusivo = value; }
        public string IdentificacaoTitulo { get => _IdentificacaoTitulo; set => _IdentificacaoTitulo = value; }
        public string Brancos2 { get => _Brancos2; set => _Brancos2 = value; }
        public string TipoOcorrencia { get => _TipoOcorrencia; set => _TipoOcorrencia = value; }
        public string CodRejeicao { get => _CodRejeicao; set => _CodRejeicao = value; }
        public string TipoCarteira { get => _TipoCarteira; set => _TipoCarteira = value; }
        public string IdentOcorrencia { get => _IdentOcorrencia; set => _IdentOcorrencia = value; }
        public string DataOcorBanco { get => _DataOcorBanco; set => _DataOcorBanco = value; }
        public string TituloEmpresa { get => _TituloEmpresa; set => _TituloEmpresa = value; }
        public string NumTitulo { get => _NumTitulo; set => _NumTitulo = value; }
        public string Brancos3 { get => _Brancos3; set => _Brancos3 = value; }
        public string VencimentoTitulo { get => _VencimentoTitulo; set => _VencimentoTitulo = value; }
        public string ValorNominalTitulo { get => _ValorNominalTitulo; set => _ValorNominalTitulo = value; }
        public string CodigoBanco { get => _CodigoBanco; set => _CodigoBanco = value; }
        public string AgenciaEncarregada { get => _AgenciaEncarregada; set => _AgenciaEncarregada = value; }
        public string EspecieTitulo { get => _EspecieTitulo; set => _EspecieTitulo = value; }
        public string TarifaCobranca { get => _TarifaCobranca; set => _TarifaCobranca = value; }
        public string OutrasDespesas { get => _OutrasDespesas; set => _OutrasDespesas = value; }
        public string Zeros { get => _Zeros; set => _Zeros = value; }
        public string ValorIOF { get => _ValorIOF; set => _ValorIOF = value; }
        public string ValorAbatimentoConcedido { get => _ValorAbatimentoConcedido; set => _ValorAbatimentoConcedido = value; }
        public string ValorDescontoConcedido { get => _ValorDescontoConcedido; set => _ValorDescontoConcedido = value; }
        public string Valorliq { get => _Valorliq; set => _Valorliq = value; }
        public string ValorJurosMora { get => _ValorJurosMora; set => _ValorJurosMora = value; }
        public string OutrosCreditos { get => _OutrosCreditos; set => _OutrosCreditos = value; }
        public string IdentificacaoMoeda { get => _IdentificacaoMoeda; set => _IdentificacaoMoeda = value; }
        public string DataCredito { get => _DataCredito; set => _DataCredito = value; }
        public string Brancos4 { get => _Brancos4; set => _Brancos4 = value; }
        public string BeneficTransf { get => _BeneficTransf; set => _BeneficTransf = value; }
        public string TituloDDA { get => _TituloDDA; set => _TituloDDA = value; }
        public string MeioLiquidacao { get => _MeioLiquidacao; set => _MeioLiquidacao = value; }
        public string Brancos5 { get => _Brancos5; set => _Brancos5 = value; }
        public string SeuNumero { get => _SeuNumero; set => _SeuNumero = value; }
        public string SequencialArquivo { get => _SequencialArquivo; set => _SequencialArquivo = value; }
        public string SequencialRegistro { get => _SequencialRegistro; set => _SequencialRegistro = value; }
        #endregion
        #endregion


        public TRegistroEDI_Safra_Retorno()
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
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0018, 004, 0, string.Empty, ' ')); //018-031
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 006, 0, string.Empty, ' ')); //032-037
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, string.Empty, ' ')); //038-062
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 009, 0, string.Empty, ' ')); //063-071
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0072, 031, 0, string.Empty, ' ')); //072-102
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0103, 002, 0, string.Empty, ' ')); //103-104
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0105, 003, 0, string.Empty, ' ')); //105-107
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0108, 001, 0, string.Empty, ' ')); //108-108
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, string.Empty, ' ')); //109-110
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 006, 0, string.Empty, ' ')); //111-116
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0117, 010, 0, string.Empty, ' ')); //117-126
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0127, 009, 0, string.Empty, ' ')); //127-135
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0136, 011, 0, string.Empty, ' ')); //136-146
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0147, 006, 0, string.Empty, ' ')); //147-152
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0153, 013, 0, string.Empty, ' ')); //153-165
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0166, 003, 0, string.Empty, ' ')); //166-168
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0169, 005, 0, string.Empty, ' ')); //169-173
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0174, 002, 0, string.Empty, ' ')); //174-175
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0176, 013, 0, string.Empty, ' ')); //176-188
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0189, 013, 0, string.Empty, ' ')); //189-201
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0202, 013, 0, string.Empty, ' ')); //202-214
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0215, 013, 0, string.Empty, ' ')); //215-227
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0228, 013, 0, string.Empty, ' ')); //228-240
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0241, 013, 0, string.Empty, ' ')); //241-253
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0254, 013, 0, string.Empty, ' ')); //254-266
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0267, 013, 0, string.Empty, ' ')); //267-279
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0280, 013, 0, string.Empty, ' ')); //280-292
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0293, 003, 0, string.Empty, ' ')); //293-295
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0296, 006, 0, string.Empty, ' ')); //296-301
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0302, 006, 0, string.Empty, ' ')); //302-307
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0308, 014, 0, string.Empty, ' ')); //308-321
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0322, 001, 0, string.Empty, ' ')); //322-322
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0323, 002, 0, string.Empty, ' ')); //323-324 
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0325, 052, 0, string.Empty, ' ')); //325-376
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0377, 015, 0, string.Empty, ' ')); //377-391
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0392, 003, 0, string.Empty, ' ')); //392-394
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0395, 006, 0, string.Empty, ' ')); //395-400
            #endregion
        }

        public override void CodificarLinha() {
            #region Todos os Campos
            //PARA LEITURA DO RETORNO BANCÁRIO NÃO PRECISAMOS IMPLEMENTAR ESSE MÉTODO           
            #endregion
            //
            base.CodificarLinha(); //Aqui que eu chamo efetivamente a rotina de codificação; o resultado será exibido na propriedade LinhaRegistro.
        }

        public override void DecodificarLinha() {
            base.DecodificarLinha();

            this.Identificacao = (string)this._CamposEDI[0].ValorNatural;
            this.InscricaoEmpresa = (string)this.CamposEDI[1].ValorNatural;
            this.NumeroInscricao = (string)this.CamposEDI[2].ValorNatural;
            this.IdentEmpresa = (string)this.CamposEDI[3].ValorNatural;
            this.Brancos1 = (string)this.CamposEDI[4].ValorNatural;
            this.Exclusivo = (string)this.CamposEDI[5].ValorNatural;
            this.IdentificacaoTitulo = (string)this.CamposEDI[6].ValorNatural;
            this.Brancos2 = (string)this.CamposEDI[7].ValorNatural;
            this.TipoOcorrencia = (string)this.CamposEDI[8].ValorNatural;
            this.CodRejeicao = (string)this.CamposEDI[9].ValorNatural;
            this.TipoCarteira = (string)this.CamposEDI[10].ValorNatural;
            this.IdentOcorrencia = (string)this.CamposEDI[11].ValorNatural;
            this.DataOcorBanco = (string)this.CamposEDI[12].ValorNatural;
            this.TituloEmpresa = (string)this.CamposEDI[13].ValorNatural;
            this.NumTitulo = (string)this.CamposEDI[14].ValorNatural;
            this.Brancos3 = (string)this.CamposEDI[15].ValorNatural;
            this.VencimentoTitulo = (string)this.CamposEDI[16].ValorNatural;
            this.ValorNominalTitulo = (string)this.CamposEDI[17].ValorNatural;
            this.CodigoBanco = (string)this.CamposEDI[18].ValorNatural;
            this.AgenciaEncarregada = (string)this.CamposEDI[19].ValorNatural;
            this.EspecieTitulo = (string)this.CamposEDI[20].ValorNatural;
            this.TarifaCobranca = (string)this.CamposEDI[21].ValorNatural;
            this.OutrasDespesas = (string)this.CamposEDI[22].ValorNatural;
            this.Zeros = (string)this.CamposEDI[23].ValorNatural;
            this.ValorIOF = (string)this.CamposEDI[24].ValorNatural; //Somente utilizado pela empresa CIA Seguros
            this.ValorAbatimentoConcedido = (string)this.CamposEDI[25].ValorNatural;
            this.ValorDescontoConcedido = (string)this.CamposEDI[26].ValorNatural;
            this.Valorliq = (string)this.CamposEDI[27].ValorNatural;
            this.ValorJurosMora = (string)this.CamposEDI[28].ValorNatural;
            this.OutrosCreditos = (string)this.CamposEDI[29].ValorNatural;
            this.IdentificacaoMoeda = (string)this.CamposEDI[30].ValorNatural;
            this.DataCredito = (string)this.CamposEDI[31].ValorNatural;
            this.Brancos4 = (string)this.CamposEDI[32].ValorNatural;
            this.BeneficTransf = (string)this.CamposEDI[33].ValorNatural;
            this.TituloDDA = (string)this.CamposEDI[34].ValorNatural;
            this.MeioLiquidacao = (string)this.CamposEDI[35].ValorNatural;
            this.Brancos5 = (string)this.CamposEDI[36].ValorNatural;
            this.SeuNumero = (string)this.CamposEDI[37].ValorNatural;
            this.SequencialArquivo = (string)this.CamposEDI[38].ValorNatural;
            this.SequencialRegistro = (string)this.CamposEDI[39].ValorNatural;            
            
        }
    }
}
