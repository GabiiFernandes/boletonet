using System;
using System.Web.UI;
using BoletoNet;
using BoletoNet.Util;
using BoletoNet.EDI.Banco;

[assembly: WebResource("BoletoNet.Imagens.422.jpg", "image/jpg")]

namespace BoletoNet
{
    /// <author>  
    /// Eduardo Frare
    /// Stiven 
    /// Diogo
    /// Miamoto
    /// </author>    

    ///<summary>
    /// Classe referente ao Banco Banco_Safra
    ///</summary>
    internal class Banco_Safra : AbstractBanco, IBanco
    {
        private string _dacNossoNumero = string.Empty;
        private int _dacContaCorrente = 0;
        private int _dacBoleto = 0;
        private int sequencialArquivo = 0;

        /// <summary>
        /// Classe responsavel em criar os campos do Banco Banco_Safra.
        /// </summary>
        internal Banco_Safra()
        {
            this.Codigo = 422;
            this.Digito = "7";
            this.Nome = "Banco_Safra";
        }

        internal int numeroDocumento = 0;

        /// <summary>
        /// Calcula o digito do Nosso Numero
        /// </summary>
        public string CalcularDigitoNossoNumero(Boleto boleto)
        {
            string sfCarteira = boleto.Carteira.ToString();


            if (boleto.NossoNumero.Length < 9)
            {
                throw new IndexOutOfRangeException("Erro. O campo 'Nosso N�mero' deve ter mais de 9 digitos. Voc� digitou " + boleto.NossoNumero);
            }
            string sfNossoNumero = boleto.NossoNumero.Substring(0, 8);
            int sfDigitoNossoNumero = Mod11(sfNossoNumero, 9, 0);
            string sfDigito = "";

            if (sfDigitoNossoNumero == 0)
                sfDigito = "1";
            else if (sfDigitoNossoNumero > 1)
                sfDigito = Convert.ToString(11 - sfDigitoNossoNumero);

            return sfDigito;

        }


        /// <summary>       
        /// SISTEMA	        020	020	7	FIXO
        /// CLIENTE	        021	034	C�DIGO DO CLIENTE	C�DIGO/AG�NCIA CEDENTE
        /// N/N�MERO	    035	043	NOSSO N�MERO	NOSSO N�MERO DO T�TULO
        /// TIPO COBRAN�A	044	044	2	FIXO
        /// </summary>
        public string CampoLivre(Boleto boleto)
        {

            return "7" + FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Agencia.ToString()+"0", 5)
                + FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Conta.ToString() + boleto.Cedente.ContaBancaria.DigitoConta, 9)
                + boleto.NossoNumero.Substring(0, 9) + "2";
        }


        #region IBanco Members
        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
        {
            try
            {
                string _header = " ";

                this.sequencialArquivo = numeroArquivoRemessa;
                base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, this.sequencialArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Fun��o n�o implementada: GerarHeaderRemessa() << CNAB240");
                    case TipoArquivo.CNAB400:
                        _header = GerarHeaderRemessaCNAB400(cedente, this.sequencialArquivo);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _header;
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a gera��o do HEADER do arquivo de REMESSA.", e);
            }
        }

        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                base.GerarDetalheRemessa(boleto, this.sequencialArquivo, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Fun��o n�o implementada: GerarDetalheRemessa() << CNAB240");
                    case TipoArquivo.CNAB400:
                        _detalhe = GerarDetalheRemessaCNAB400(boleto, this.sequencialArquivo);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe;

            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a gera��o do DETALHE arquivo de REMESSA.", e);
            }
        }

        public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
        {
            try
            {
                string _trailer = " ";

                base.GerarTrailerRemessa(numeroRegistro, tipoArquivo, cedente, vltitulostotal);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Fun��o n�o implementada: GerarTrailerRemessa() << CNAB240");
                    case TipoArquivo.CNAB400:
                        _trailer = GerarTrailerRemessa400(numeroRegistro, 0);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _trailer;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a gera��o do TRAILER do arquivo de REMESSA.", ex);
            }
        }

        #region Retorno
        DetalheRetorno IBanco.LerDetalheRetornoCNAB400(string registro)
        {
            try
            {
                TRegistroEDI_Safra_Retorno reg = new TRegistroEDI_Safra_Retorno();
                reg.LinhaRegistro = registro;
                reg.DecodificarLinha();

                //Passa para o detalhe as propriedades de reg;
                DetalheRetorno detalhe = new DetalheRetorno(registro);

                detalhe.Agencia = Utils.ToInt32(String.Concat(reg.Agencia, reg.DigitoAgencia));
                detalhe.Conta = Utils.ToInt32(reg.Conta);
                detalhe.DACConta = Utils.ToInt32(reg.DigitoConta);
                //detalhe.NossoNumeroComDV = reg.NossoNumero;
                //detalhe.NossoNumero = reg.NossoNumero.Substring(0, reg.NossoNumero.Length - 1); //Nosso N�mero sem o DV!
                //detalhe.DACNossoNumero = reg.NossoNumero.Substring(reg.NossoNumero.Length - 1); //DV
                detalhe.Carteira = reg.TipoCarteira;
                detalhe.CodigoOcorrencia = Utils.ToInt32(reg.TipoOcorrencia);
                detalhe.DescricaoOcorrencia = new CodigoMovimento(this.Codigo, detalhe.CodigoOcorrencia).Descricao;
                int dataLiquidacao = Utils.ToInt32(reg.DataLimiteDesconto);
                detalhe.DataLiquidacao = Utils.ToDateTime(dataLiquidacao.ToString("##-##-##"));
                detalhe.NumeroDocumento = reg.IdentificacaoTitulo;
                int dataVencimento = Utils.ToInt32(reg.VencimentoTitulo);
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                detalhe.ValorTitulo = Utils.ToDecimal(reg.ValorNominalTitulo) / 100;
                detalhe.CodigoBanco = Utils.ToInt32(reg.CodigoBanco);
                detalhe.AgenciaCobradora = Utils.ToInt32(reg.Agencia);
                detalhe.Especie = Utils.ToInt32(reg.EspecieTitulo);
                //int dataCredito = Utils.ToInt32(reg.DataCredito);
                //detalhe.DataOcorrencia = Utils.ToDateTime(dataCredito.ToString("##-##-##"));
                //detalhe.TarifaCobranca = Utils.ToDecimal(reg.ValorJurosMora) / 100;
                //detalhe.OutrasDespesas = Utils.ToDecimal(reg.OutrasDespesas) / 100;
                //detalhe.ValorOutrasDespesas = Utils.ToDecimal(reg.valor) / 100;
                detalhe.IOF = Utils.ToDecimal(reg.ValorIOF) / 100;
                detalhe.Abatimentos = Utils.ToDecimal(reg.ValorAbatimentoConcedido) / 100;
                detalhe.Descontos = Utils.ToDecimal(reg.ValorDescontoConcedido) / 100;
                //detalhe.ValorPrincipal = Utils.ToDecimal(reg.valor) / 100;
                detalhe.JurosMora = Utils.ToDecimal(reg.ValorJurosMora) / 100;
                //detalhe.OutrosCreditos = Utils.ToDecimal(reg.OutrosRecebimentos) / 100;
                //detalhe.ValorPago = Utils.ToDecimal(reg.ValorLancamento) / 100;

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }
        #endregion

        public override void ValidaBoleto(Boleto boleto)
        {

            // Calcula o DAC do Nosso N�mero
            _dacNossoNumero = CalcularDigitoNossoNumero(boleto);

            // Calcula o DAC da Conta Corrente
            _dacContaCorrente = Mod10(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta);

            //Verifica se o nosso n�mero � v�lido
            if (Utils.ToInt64(boleto.NossoNumero) == 0)
                throw new NotImplementedException("Nosso n�mero inv�lido");

            //Verifica se data do processamento � valida
			//if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
			if (boleto.DataProcessamento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataProcessamento = DateTime.Now;

            //Verifica se data do documento � valida
			//if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
			if (boleto.DataDocumento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataDocumento = DateTime.Now;

            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }

        public override void FormataNumeroDocumento(Boleto boleto)
        {
            throw new NotImplementedException("Fun��o n�o implementada.");
        }

        public override void FormataNossoNumero(Boleto boleto)
        {
            //throw new NotImplementedException("Fun��o n�o implementada.");
        }

        /// <summary>
        ///	O c�digo de barra para cobran�a cont�m 44 posi��es dispostas da seguinte forma:
        ///    01 a 03 - 3 - Identifica��o  do  Banco
        ///    04 a 04 - 1 - C�digo da Moeda
        ///    05 a 05 � 1 - D�gito verificador do C�digo de Barras
        ///    06 a 19 - 14 - Valor
        ///    20 a 44 � 25 - Campo Livre
        /// </summary>
        public override void FormataCodigoBarra(Boleto boleto)
        {
            boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}",
                Codigo, boleto.Moeda, 'D', FatorVencimento(boleto), GetValorBoletoFormatado(boleto.ValorBoleto), CampoLivre(boleto));
            _dacBoleto = GetDacBoleto(boleto.CodigoBarra.Codigo);
            boleto.CodigoBarra.Codigo = boleto.CodigoBarra.Codigo.Replace("D", _dacBoleto.ToString());
        }


        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            string agencia = FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Agencia.ToString(), 4);
            string conta = FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Conta.ToString()+ boleto.Cedente.ContaBancaria.DigitoConta.ToString(), 9);
            string nossoNumero = boleto.NossoNumero.Substring(0, 9);
            int[] digitosVerificadores;
            int asciiZero = 48;
            boleto.CodigoBarra.LinhaDigitavel = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}", 
                this.Codigo, boleto.Moeda, 7, agencia, 'X', 0, conta, 'Y', nossoNumero, 2, 'Z', _dacBoleto.ToString(), FatorVencimento(boleto), GetValorBoletoFormatado(boleto.ValorBoleto));

            digitosVerificadores = GetDigitosVerificadoresLinhaDigitavel(boleto.CodigoBarra.LinhaDigitavel);
            boleto.CodigoBarra.LinhaDigitavel = boleto.CodigoBarra.LinhaDigitavel
                .Replace('X', Convert.ToChar(asciiZero + digitosVerificadores[0]))
                .Replace('Y', Convert.ToChar(asciiZero + digitosVerificadores[1]))
                .Replace('Z', Convert.ToChar(asciiZero + digitosVerificadores[2]));
        }
        #endregion IBanco Members



        #region Arquivo Remessa 400
        public string GerarHeaderRemessaCNAB400(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "0", '0'));                                   //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 001, 0, "1", '0'));                                   //002-002
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0003, 007, 0, "REMESSA", ' '));                             //003-009
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0010, 002, 0, "01", '0'));                                  //010-011
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0012, 008, 0, "COBRANCA", ' '));                            //012-019
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0020, 007, 0, string.Empty, ' '));                          //020-026
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0027, 004, 0, cedente.ContaBancaria.Agencia, '0'));         //027-030
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, cedente.ContaBancaria.DigitoAgencia, '0'));   //031-031
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 008, 0, cedente.ContaBancaria.Conta, '0'));           //032-039
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0040, 001, 0, cedente.ContaBancaria.DigitoConta, '0'));     //040-040
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0041, 006, 0, string.Empty, ' '));                          //041-046
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0047, 030, 0, cedente.Nome.ToUpper(), ' '));                //047-076
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0077, 018, 0, "422BANCO SAFRA", ' '));                      //077-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0095, 006, 0, DateTime.Now, '0'));                          //095-100
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0101, 291, 0, string.Empty, ' '));                          //101-391
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0392, 003, 0, numeroArquivoRemessa, '0'));                  //392-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0395, 006, 0, this.ObtainNumeroDocumento(), '0'));          //395-400

                reg.CodificarLinha();

                string vLinha = reg.LinhaRegistro;
                string _header = Utils.SubstituiCaracteresEspeciais(vLinha);

                return _header;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", e);
            }
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            int numeroDias = 0;
            int instrucao1 = 0;
            int instrucao2 = 0;
            try
            {
                /*
                 * C�digo de Movimento Remessa 
                 * 01 - Registro de t�tulos;  
                 * 02 - Solicita��o de baixa; 
                 * 04 - Concess�o de abatimento;  
                 * 05 - Cancelamento de abatimento;  
                 * 06 - Altera��o de vencimento de t�tulo;  
                 * 09 - Instru��es para protestar (Nota 09);   
                 * 10 - Instru��o para sustar protesto;  
                 * 12 - Altera��o de nome e endere�o do Pagador;  
                 * 17 � Liquida��o de t�tulo n�o registro ou pagamento em duplicidade; 
                 * 31 - Conceder desconto; 
                 * 32 - N�o conceder desconto. 
                 */

                string tipoInscricaoEmitente = "02";   // Padr�o CNPJ
                string tipoInscricaoSacado;
                if (boleto.Sacado.CPFCNPJ.Length.Equals(11))
                    tipoInscricaoSacado = "01"; //CPF
                else
                    tipoInscricaoSacado = "02";   //CNPJ

                TRegistroEDI reg = new TRegistroEDI();

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "1", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 002, 0, tipoInscricaoEmitente, '0'));                     //002-003
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0004, 014, 0, boleto.Cedente.CPFCNPJ, '0'));                    //004-017
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0018, 004, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //018-021
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 001, 0, boleto.Cedente.ContaBancaria.DigitoAgencia, '0'));//022-022
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0023, 008, 0, boleto.Cedente.ContaBancaria.Conta, '0'));        //023-030
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, boleto.Cedente.ContaBancaria.DigitoConta, '0'));  //031-031
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 006, 0, string.Empty, ' '));                              //032-037
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, string.Empty, ' '));                              //038-062
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 009, 0, boleto.NossoNumero, '0'));            //063-071
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0072, 030, 0, string.Empty, ' '));                              //072-101
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0102, 001, 0, "0", '0'));                                       //102-102
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0103, 002, 0, "00", '0'));                                      //103-104
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0105, 001, 0, string.Empty, ' '));                              //105-105
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0106, 002, 0, numeroDias, '0'));                                //106-107
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0108, 001, 0, boleto.Carteira, '0'));                           //108-108
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, "01", '0'));                                      //109-110
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 010, 0, boleto.NumeroDocumento, '0'));                    //111-120
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0121, 006, 0, boleto.DataVencimento, '0'));                     //121-126
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0127, 013, 0, boleto.ValorBoleto.ApenasNumeros(), '0'));                        //127-139
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0140, 003, 0, "422", '0'));                                     //140-142
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0143, 005, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //143-147
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0148, 002, 0, boleto.Especie, '0'));                            //148-149
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0150, 001, 0, boleto.Aceite, '0'));                             //150-150
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0151, 006, 0, DateTime.Now, '0'));                              //151-156

                //PRIMEIRA INSTRU��O DE COBRAN�A
                foreach (IInstrucao instrucao in boleto.Instrucoes)
                        instrucao1 = instrucao.Codigo; //Deve conter somente uma instrucao de cobranca

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0157, 002, 0, instrucao1, '0'));                                //157-158
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0159, 002, 0, instrucao2, '0'));                                //159-160
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0161, 013, 0, boleto.JurosMora.ApenasNumeros(), '0'));          //161-173
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0174, 006, 0, boleto.DataDesconto, '0'));                       //174-179
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0180, 013, 0, boleto.ValorDesconto.ApenasNumeros(), '0'));                      //180-192
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0193, 013, 0, "0", '0'));                                       //193-205
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0206, 006, 0, boleto.DataVencimento.AddDays(1), '0'));          //206-211
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0212, 004, 0, boleto.PercMulta.ApenasNumeros(), '0'));                      //212-215
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0216, 003, 0, 0, '0'));                                         //216-218
                //PAGADOR
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0219, 002, 0, tipoInscricaoSacado, '0'));                       //219-220
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0221, 014, 0, boleto.Sacado.CPFCNPJ, '0'));                     //221-234
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0235, 040, 0, boleto.Sacado.Nome, ' '));                        //235-274
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0275, 040, 0, boleto.Sacado.Endereco.EndComNumero, ' '));       //275-314 
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0315, 010, 0, boleto.Sacado.Endereco.Bairro, ' '));             //315-324
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0325, 002, 0, string.Empty, ' '));                              //325-326
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0327, 008, 0, boleto.Sacado.Endereco.CEP, '0'));                //327-334
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0335, 015, 0, boleto.Sacado.Endereco.Cidade, ' '));             //335-349
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0350, 002, 0, boleto.Sacado.Endereco.UF, ' '));                 //350-351
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0352, 030, 0, string.Empty, ' '));                              //352-381
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0382, 007, 0, string.Empty, ' '));                              //382-388
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0389, 003, 0, "422", '0'));                                     //389-391
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0392, 003, 0, numeroRegistro, '0'));                            //392-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0395, 006, 0, this.ObtainNumeroDocumento(), '0'));              //395-400

                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", e);
            }
        }

        public string GerarTrailerRemessa400(int numeroRegistro, decimal vltitulostotal)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0001, 001, 0, "9", ' '));                           //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 367, 0, string.Empty, ' '));                  //002-368
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0369, 008, 0, numeroRegistro-2, '0'));              //369-376 Header e Trailler tbm contam como registros, ent�o fazemos -2 para deduzir os dois. Aqui queremos s� a qtd de detalhes da remessa
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0377, 015, 0, vltitulostotal, '0'));                //377-391
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0392, 003, 0, this.sequencialArquivo, '0'));          //392-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0395, 006, 0, this.ObtainNumeroDocumento(), '0'));  //395-400

                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a gera��o do registro TRAILER do arquivo de REMESSA.", e);
            }
        }
        #endregion



        /// <summary>
        /// Efetua as Valida��es dentro da classe Boleto, para garantir a gera��o da remessa
        /// </summary>
        public override bool ValidarRemessa(TipoArquivo tipoArquivo, string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, int numeroArquivoRemessa, out string mensagem)
        {
            bool vRetorno = true;
            string vMsg = string.Empty;
            ////IMPLEMENTACAO PENDENTE...
            mensagem = vMsg;
            return vRetorno;
        }


        public string GerarDetalheMultaRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            throw new NotImplementedException();
        }


        #region Metodos internos
        internal static string GetFormatedNossoNumero(Boleto boleto)
        {
            string _nossoNumero = string.Format("{0}{1}",
                Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta, 8),
                Utils.FormatCode(boleto.NossoNumero, 9));
            return _nossoNumero;
        }

        internal int ObtainNumeroDocumento()
        {
            int numeroDocumento = this.numeroDocumento + 1;
            this.numeroDocumento = numeroDocumento;

            return numeroDocumento;
        }

        internal int GetDacBoleto(string codigoBarras)
        {
            char[] digitosBarras;
            digitosBarras = codigoBarras.Replace("D", String.Empty).ToCharArray(0, 43); //Deve conter o caractere 'D' na posicao onde se encontra o digito DAC
            int dac;

            int mult = 1;
            int sum = 0;

            for (int i = 43; i >= 1; i--)
            {
                mult++;
                if (mult > 9)
                    mult = 2;

                sum += (int)Char.GetNumericValue(digitosBarras[i - 1]) * mult;
            }

            dac = 11 - (sum % 11);
            if (dac >= 10)
                dac = 1;

            return dac;
        }

        internal int[] GetDigitosVerificadoresLinhaDigitavel(string linhaDigitavel)
        {
            string primeiraLinha = linhaDigitavel.Substring(0, linhaDigitavel.IndexOf('X'));
            string segundaLinha = linhaDigitavel.Substring(linhaDigitavel.IndexOf('X') + 1, linhaDigitavel.IndexOf('Y') - linhaDigitavel.IndexOf('X') - 1);
            string terceiraLinha = linhaDigitavel.Substring(linhaDigitavel.IndexOf('Y') + 1, linhaDigitavel.IndexOf('Z') - linhaDigitavel.IndexOf('Y') - 1);
            int[] verificadores = new int[3];

            verificadores[0] = CalcularDigitoLinhaDigitavel(primeiraLinha);
            verificadores[1] = CalcularDigitoLinhaDigitavel(segundaLinha);
            verificadores[2] = CalcularDigitoLinhaDigitavel(terceiraLinha);

            return verificadores;
        }

        internal int CalcularDigitoLinhaDigitavel(string sequenciaNumerica)
        {
            char[] digitos = sequenciaNumerica.ToCharArray(0, sequenciaNumerica.Length);
            int digitosMultSum = 0;
            bool even = (sequenciaNumerica.Length % 2) > 0;
            int aux;
            int divisao;

            foreach (char digito in digitos)
            {
                if (even)
                {
                    aux = ((int)Char.GetNumericValue(digito)*2);
                }
                else
                {
                    aux = ((int)Char.GetNumericValue(digito)*1);
                }
                digitosMultSum += (aux >= 10) ? aux-9 : aux;
                even = !even;
            }

            divisao = (digitosMultSum % 10);
            return (divisao % 10 == 0) ? 0 : (10-divisao);
        }

        internal string GetValorBoletoFormatado(decimal valor)
        {
            string valorBoleto = valor.ToString("f").Replace(",", "").Replace(".", "");
            return FormatZerosEsquerda(valorBoleto, 10);
        }

        internal string FormatZerosEsquerda(string texto, int tamanhoMax)
        {
            string resultado = "";
            for(int i = texto.Length; i < tamanhoMax; i++)
            {
                resultado += "0";
            }
            resultado += texto;
            return resultado;
        }
        #endregion
    }
}
