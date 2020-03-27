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
        private string _Agencia;
        private string _DigitoAgencia;
        private string _Conta;
        private string _DigitoConta;
        private string _Brancos1;
        private string _Exclusivo;
        private string _IdentificacaoTitulo;
        private string _Brancos2;
        private string _CodigoIOF;
        private string _IdentificacaoMoeda;
        private string _Brancos3;
        private string _TerceiraInstrucaoCobr;
        private string _TipoCarteira;
        private string _TipoOcorrencia;
        private string _TituloEmpresa;
        private string _VencimentoTitulo;
        private string _ValorNominalTitulo;
        private string _CodigoBanco;
        private string _AgenciaEncarregada;
        private string _EspecieTitulo;
        private string _AceiteTitulo;
        private string _EmissaoTitulo;
        private string _PrimeiraInstrucaoCobr;
        private string _SegundaInstrucaoCobr;
        private string _ValorJurosMora;
        private string _DataLimiteDesconto;
        private string _ValorDescontoConcedido;
        private string _ValorIOF;
        private string _ValorAbatimentoConcedido;
        private string _PagadorCodigoInscricao;
        private string _PagadorCPF_CNPJ;
        private string _PagadorNome;
        private string _PagadorEndereco;
        private string _PagadorBairro;
        private string _Brancos4;
        private string _PagadorCEP;
        private string _PagadorCidade;
        private string _PagadorEstadoCigla;
        private string _SacadorAvalistaNome;
        private string _Brancos5;
        private string _BancoEmitente;
        private string _SequencialArquivo;
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
        public string Brancos1 { get => _Brancos1; set => _Brancos1 = value; }
        public string Exclusivo { get => _Exclusivo; set => _Exclusivo = value; }
        public string IdentificacaoTitulo { get => _IdentificacaoTitulo; set => _IdentificacaoTitulo = value; }
        public string Brancos2 { get => _Brancos2; set => _Brancos2 = value; }
        public string CodigoIOF { get => _CodigoIOF; set => _CodigoIOF = value; }
        public string IdentificacaoMoeda { get => _IdentificacaoMoeda; set => _IdentificacaoMoeda = value; }
        public string Brancos3 { get => _Brancos3; set => _Brancos3 = value; }
        public string TerceiraInstrucaoCobr { get => _TerceiraInstrucaoCobr; set => _TerceiraInstrucaoCobr = value; }
        public string TipoCarteira { get => _TipoCarteira; set => _TipoCarteira = value; }
        public string TipoOcorrencia { get => _TipoOcorrencia; set => _TipoOcorrencia = value; }
        public string TituloEmpresa { get => _TituloEmpresa; set => _TituloEmpresa = value; }
        public string VencimentoTitulo { get => _VencimentoTitulo; set => _VencimentoTitulo = value; }
        public string ValorNominalTitulo { get => _ValorNominalTitulo; set => _ValorNominalTitulo = value; }
        public string CodigoBanco { get => _CodigoBanco; set => _CodigoBanco = value; }
        public string AgenciaEncarregada { get => _AgenciaEncarregada; set => _AgenciaEncarregada = value; }
        public string EspecieTitulo { get => _EspecieTitulo; set => _EspecieTitulo = value; }
        public string AceiteTitulo { get => _AceiteTitulo; set => _AceiteTitulo = value; }
        public string EmissaoTitulo { get => _EmissaoTitulo; set => _EmissaoTitulo = value; }
        public string PrimeiraInstrucaoCobr { get => _PrimeiraInstrucaoCobr; set => _PrimeiraInstrucaoCobr = value; }
        public string SegundaInstrucaoCobr { get => _SegundaInstrucaoCobr; set => _SegundaInstrucaoCobr = value; }
        public string ValorJurosMora { get => _ValorJurosMora; set => _ValorJurosMora = value; }
        public string DataLimiteDesconto { get => _DataLimiteDesconto; set => _DataLimiteDesconto = value; }
        public string ValorDescontoConcedido { get => _ValorDescontoConcedido; set => _ValorDescontoConcedido = value; }
        public string ValorIOF { get => _ValorIOF; set => _ValorIOF = value; }
        public string ValorAbatimentoConcedido { get => _ValorAbatimentoConcedido; set => _ValorAbatimentoConcedido = value; }
        public string PagadorCodigoInscricao { get => _PagadorCodigoInscricao; set => _PagadorCodigoInscricao = value; }
        public string PagadorCPF_CNPJ { get => _PagadorCPF_CNPJ; set => _PagadorCPF_CNPJ = value; }
        public string PagadorNome { get => _PagadorNome; set => _PagadorNome = value; }
        public string PagadorEndereco { get => _PagadorEndereco; set => _PagadorEndereco = value; }
        public string PagadorBairro { get => _PagadorBairro; set => _PagadorBairro = value; }
        public string Brancos4 { get => _Brancos4; set => _Brancos4 = value; }
        public string PagadorCEP { get => _PagadorCEP; set => _PagadorCEP = value; }
        public string PagadorCidade { get => _PagadorCidade; set => _PagadorCidade = value; }
        public string PagadorEstadoCigla { get => _PagadorEstadoCigla; set => _PagadorEstadoCigla = value; }
        public string SacadorAvalistaNome { get => _SacadorAvalistaNome; set => _SacadorAvalistaNome = value; }
        public string Brancos5 { get => _Brancos5; set => _Brancos5 = value; }
        public string BancoEmitente { get => _BancoEmitente; set => _BancoEmitente = value; }
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
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0018, 004, 0, string.Empty, ' ')); //018-021
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 001, 0, string.Empty, ' ')); //022-022
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0023, 008, 0, string.Empty, ' ')); //023-030
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, string.Empty, ' ')); //031-031
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 006, 0, string.Empty, ' ')); //032-037
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, string.Empty, ' ')); //038-062
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 009, 0, string.Empty, ' ')); //063-071
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0072, 030, 0, string.Empty, ' ')); //072-101
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0102, 001, 0, string.Empty, ' ')); //102-102
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0103, 002, 0, string.Empty, ' ')); //103-104
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0105, 001, 0, string.Empty, ' ')); //105-105
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0106, 002, 0, string.Empty, ' ')); //106-107
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0108, 001, 0, string.Empty, ' ')); //108-108
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, string.Empty, ' ')); //109-110
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 010, 0, string.Empty, ' ')); //111-120
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0121, 006, 0, string.Empty, ' ')); //121-126
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0127, 013, 0, string.Empty, ' ')); //127-139
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0140, 003, 0, string.Empty, ' ')); //140-142
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0143, 005, 0, string.Empty, ' ')); //143-147
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0148, 002, 0, string.Empty, ' ')); //148-149
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0150, 001, 0, string.Empty, ' ')); //150-150
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0151, 006, 0, string.Empty, ' ')); //151-156
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0157, 002, 0, string.Empty, ' ')); //157-158
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0159, 002, 0, string.Empty, ' ')); //159-160
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0161, 013, 0, string.Empty, ' ')); //161-173
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0174, 006, 0, string.Empty, ' ')); //174-179
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0180, 013, 0, string.Empty, ' ')); //180-192
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0193, 013, 0, string.Empty, ' ')); //193-205
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0206, 013, 0, string.Empty, ' ')); //206-218
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0219, 002, 0, string.Empty, ' ')); //219-220
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0221, 014, 0, string.Empty, ' ')); //221-234
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0235, 040, 0, string.Empty, ' ')); //235-274
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0275, 040, 0, string.Empty, ' ')); //275-314 
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0315, 010, 0, string.Empty, ' ')); //315-324
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0325, 002, 0, string.Empty, ' ')); //325-326
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0327, 008, 0, string.Empty, ' ')); //327-334
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0335, 005, 0, string.Empty, ' ')); //335-349
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0350, 002, 0, string.Empty, ' ')); //350-351
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0352, 030, 0, string.Empty, ' ')); //352-381
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0382, 007, 0, string.Empty, ' ')); //382-388
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0389, 003, 0, string.Empty, ' ')); //389-391
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
            this.Agencia = (string)this.CamposEDI[3].ValorNatural;
            this.DigitoAgencia = (string)this.CamposEDI[4].ValorNatural;
            this.Conta = (string)this.CamposEDI[5].ValorNatural;
            this.DigitoConta = (string)this.CamposEDI[6].ValorNatural;
            this.Brancos1 = (string)this.CamposEDI[7].ValorNatural;
            this.Exclusivo = (string)this.CamposEDI[8].ValorNatural;
            this.IdentificacaoTitulo = (string)this.CamposEDI[9].ValorNatural;
            this.Brancos2 = (string)this.CamposEDI[10].ValorNatural;
            this.CodigoIOF = (string)this.CamposEDI[11].ValorNatural; //Somente utilizado pela empresa CIA Seguros
            this.IdentificacaoMoeda = (string)this.CamposEDI[12].ValorNatural;
            this.Brancos3 = (string)this.CamposEDI[13].ValorNatural;
            this.TerceiraInstrucaoCobr = (string)this.CamposEDI[14].ValorNatural;
            this.TipoCarteira = (string)this.CamposEDI[15].ValorNatural;
            this.TipoOcorrencia = (string)this.CamposEDI[16].ValorNatural;
            this.TituloEmpresa = (string)this.CamposEDI[17].ValorNatural;
            this.VencimentoTitulo = (string)this.CamposEDI[18].ValorNatural;
            this.ValorNominalTitulo = (string)this.CamposEDI[19].ValorNatural;
            this.CodigoBanco = (string)this.CamposEDI[20].ValorNatural;
            this.AgenciaEncarregada = (string)this.CamposEDI[21].ValorNatural;
            this.EspecieTitulo = (string)this.CamposEDI[22].ValorNatural;
            this.AceiteTitulo = (string)this.CamposEDI[23].ValorNatural;
            this.EmissaoTitulo = (string)this.CamposEDI[24].ValorNatural;
            this.PrimeiraInstrucaoCobr = (string)this.CamposEDI[25].ValorNatural;
            this.SegundaInstrucaoCobr = (string)this.CamposEDI[26].ValorNatural;
            this.ValorJurosMora = (string)this.CamposEDI[27].ValorNatural;
            this.DataLimiteDesconto = (string)this.CamposEDI[28].ValorNatural;
            this.ValorDescontoConcedido = (string)this.CamposEDI[29].ValorNatural;
            this.ValorIOF = (string)this.CamposEDI[30].ValorNatural; //Somente utilizado pela empresa CIA Seguros
            this.ValorAbatimentoConcedido = (string)this.CamposEDI[31].ValorNatural;
            this.PagadorCodigoInscricao = (string)this.CamposEDI[32].ValorNatural;
            this.PagadorCPF_CNPJ = (string)this.CamposEDI[33].ValorNatural;
            this.PagadorNome = (string)this.CamposEDI[34].ValorNatural;
            this.PagadorEndereco = (string)this.CamposEDI[35].ValorNatural;
            this.PagadorBairro = (string)this.CamposEDI[36].ValorNatural;
            this.Brancos4 = (string)this.CamposEDI[37].ValorNatural;
            this.PagadorCEP = (string)this.CamposEDI[38].ValorNatural;
            this.PagadorCidade = (string)this.CamposEDI[39].ValorNatural;
            this.PagadorEstadoCigla = (string)this.CamposEDI[40].ValorNatural;
            this.SacadorAvalistaNome = (string)this.CamposEDI[41].ValorNatural;
            this.Brancos5 = (string)this.CamposEDI[42].ValorNatural;
            this.BancoEmitente = (string)this.CamposEDI[43].ValorNatural;
            this.SequencialArquivo = (string)this.CamposEDI[44].ValorNatural;
            this.SequencialRegistro = (string)this.CamposEDI[45].ValorNatural;
        }
    }
}
