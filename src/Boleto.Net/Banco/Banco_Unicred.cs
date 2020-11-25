using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using BoletoNet;
using BoletoNet.Util;
using BoletoNet.EDI.Banco;

namespace BoletoNet
{
    internal class Banco_Unicred : AbstractBanco, IBanco
    {
        private string _dacNossoNumero = string.Empty;
        private int _dacContaCorrente = 0;
        private int _dacBoleto = 0;
        private int sequencialArquivo = 0;

        internal Banco_Unicred()
        {
            this.Codigo = 136;
            this.Digito = "8";
            this.Nome = "Banco_Unicred";
        }

        internal int numeroDocumento = 0;

        public string CalcularDigitoNossoNumero(Boleto boleto)
        {
            string Carteira = boleto.Carteira.ToString();

            if (boleto.NossoNumero.Length < 9)
            {
                throw new IndexOutOfRangeException("Erro. O campo 'Nosso Número' deve ter mais de 9 digitos. Você digitou " + boleto.NossoNumero);
            }
            string NossoNumero = boleto.NossoNumero.Substring(0, 10);
            int DigitoNossoNumero = GetDVNossoNumero(NossoNumero);
            string Digito = Convert.ToString(DigitoNossoNumero);
            return Digito;
        }

        internal int GetDVNossoNumero(string nossoNumero)
        {
            char[] digitosNossoNumero;
            digitosNossoNumero = nossoNumero.ToCharArray(0, 9);
            int DVNossoNumero;

            int mult = 1;
            int sum = 0;

            for (int i = 9; i >= 1; i--)
            {
                mult++;
                if (mult > 9)
                    mult = 2;

                sum += (int)Char.GetNumericValue(digitosNossoNumero[i - 1]) * mult;
            }

            DVNossoNumero = 11 - (sum % 11);
            if (DVNossoNumero >= 10)
                DVNossoNumero = 0;

            return DVNossoNumero;
        }
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
                        throw new Exception("Função não implementada: GerarHeaderRemessa() << CNAB240");
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
                throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", e);
            }
        }

        public string GerarHeaderRemessaCNAB400(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "0", '0'));                                    //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 001, 0, "1", '0'));                                    //002-002
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0003, 007, 0, "REMESSA", ' '));                              //003-009
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0010, 002, 0, "01", '0'));                                   //010-011
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0012, 008, 0, "COBRANCA", ' '));                             //012-026
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0027, 020, 0, cedente.Codigo + cedente.DigitoCedente, '0')); //027-046
                //reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0047, 020, 0, , ' '));                                       //047-076
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0077, 003, 0, 136, ' '));                                    //077-079
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0080, 015, 0, "UNICRED", ' '));                              //080-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0095, 006, 0, DateTime.Now, '0'));                           //095-100
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0101, 007, 0, "BRANCO", ' '));                               //101-107
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0108, 003, 0, "000", ' '));                                  //108-110
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 007, 0, "0000001", ' '));                              //111-117
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0118, 277, 0, "BRANCO", ' '));                               //118-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0395, 006, 0, "000001", ' '));                               //395-400

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

        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                base.GerarDetalheRemessa(boleto, this.sequencialArquivo, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Função não implementada: GerarDetalheRemessa() << CNAB240");
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
                throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", e);
            }
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            int numeroDias = 0;
            int instrucao1 = 0;
            int instrucao2 = 0;
            try
            {
      
                string tipoInscricaoEmitente = "02";   // Padrão CNPJ
                string tipoInscricaoSacado = "02";   // Padrão CNPJ

                TRegistroEDI reg = new TRegistroEDI();

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "1", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 005, 0, tipoInscricaoEmitente, '0'));                     //002-006
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0007, 001, 0, boleto.Cedente.CPFCNPJ, '0'));                    //007-007
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0008, 012, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //008-019
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0020, 001, 0, boleto.Cedente.ContaBancaria.DigitoAgencia, '0'));//020-020
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0021, 001, 0, boleto.Cedente.ContaBancaria.Conta, '0'));        //021-021
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 003, 0, boleto.Cedente.ContaBancaria.DigitoConta, '0'));  //022-024
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0025, 013, 0, string.Empty, ' '));                              //025-037
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, string.Empty, ' '));                              //038-062
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 003, 0, boleto.NossoNumero, '0'));                        //063-065
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0066, 002, 0, string.Empty, ' '));                              //066-067
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0068, 025, 0, "0", '0'));                                       //068-092
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0093, 001, 0, "00", '0'));                                      //093-093
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0094, 001, 0, string.Empty, ' '));                              //094-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0095, 010, 0, numeroDias, '0'));                                //095-104
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0105, 001, 0, boleto.Carteira, '0'));                           //105-105
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0106, 001, 0, boleto.Carteira, '0'));                           //106-106
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0107, 002, 0, "01", '0'));                                      //107-108
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, boleto.NumeroDocumento, '0'));                    //109-110
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0111, 010, 0, boleto.DataVencimento, '0'));                     //111-120
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0121, 006, 0, boleto.ValorBoleto.ApenasNumeros(), '0'));        //121-126
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0127, 013, 0, "422", '0'));                                     //127-139
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0140, 010, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //140-149
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0150, 001, 0, boleto.Especie, '0'));                            //150-150
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0151, 006, 0, boleto.Aceite, '0'));                             //151-156

                //PRIMEIRA INSTRUÇÃO DE COBRANÇA
                foreach (IInstrucao instrucao in boleto.Instrucoes)
                    instrucao1 = instrucao.Codigo; //Deve conter somente uma instrucao de cobranca

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0157, 001, 0, instrucao1, '0'));                                //157-157
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0158, 001, 0, instrucao2, '0'));                                //158-158
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0159, 002, 0, boleto.JurosMora.ApenasNumeros(), '0'));         //159-160 
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0161, 013, 0, boleto.DataDesconto, '0'));                       //161-173
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0174, 006, 0, boleto.ValorDesconto.ApenasNumeros(), '0'));      //174-179
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0180, 013, 0, "0", '0'));                                       //180-192
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0193, 011, 0, boleto.DataVencimento.AddDays(1), '0'));          //193-203
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0204, 002, 0, boleto.PercMulta.ApenasNumeros(), '0'));          //204-205
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0206, 013, 0, 0, '0'));                                         //206-218
                //PAGADOR
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0219, 002, 0, tipoInscricaoSacado, '0'));                       //219-220
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0221, 014, 0, boleto.Sacado.CPFCNPJ, '0'));                     //221-234
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0235, 040, 0, boleto.Sacado.Nome, ' '));                        //235-274
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0275, 040, 0, boleto.Sacado.Endereco.EndComNumero, ' '));       //275-314
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0315, 012, 0, boleto.Sacado.Endereco.Bairro, ' '));             //315-326
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0327, 008, 0, string.Empty, ' '));                              //327-334
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0335, 020, 0, boleto.Sacado.Endereco.CEP, '0'));                //335-354
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0355, 002, 0, boleto.Sacado.Endereco.Cidade, ' '));             //355-356
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0357, 038, 0, boleto.Sacado.Endereco.UF, ' '));                 //357-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0395, 006, 0, string.Empty, ' '));                              //395-400


                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", e);
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
                        throw new Exception("Função não implementada: GerarTrailerRemessa() << CNAB240");
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
                throw new Exception("Erro durante a geração do TRAILER do arquivo de REMESSA.", ex);
            }
        }

        public string GerarTrailerRemessa400(int numeroRegistro, decimal vltitulostotal)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0001, 001, 0, "9", ' '));                           //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 394, 0, string.Empty, ' '));                  //002-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0395, 006, 0, "000000", '0'));                      //395-400

                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", e);
            }
        }

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
                detalhe.Carteira = reg.TipoCarteira;
                detalhe.CodigoOcorrencia = Utils.ToInt32(reg.TipoOcorrencia);
                detalhe.DescricaoOcorrencia = new CodigoMovimento(85, detalhe.CodigoOcorrencia).Descricao;
                int dataLiquidacao = Utils.ToInt32(reg.DataLimiteDesconto);
                detalhe.DataLiquidacao = Utils.ToDateTime(dataLiquidacao.ToString("##-##-##"));
                detalhe.NumeroDocumento = reg.IdentificacaoTitulo;
                int dataVencimento = Utils.ToInt32(reg.VencimentoTitulo);
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                detalhe.ValorTitulo = Utils.ToDecimal(reg.ValorNominalTitulo) / 100;
                detalhe.CodigoBanco = Utils.ToInt32(reg.CodigoBanco);
                detalhe.AgenciaCobradora = Utils.ToInt32(reg.Agencia);
                detalhe.Especie = Utils.ToInt32(reg.EspecieTitulo);
                detalhe.IOF = Utils.ToDecimal(reg.ValorIOF) / 100;
                detalhe.Abatimentos = Utils.ToDecimal(reg.ValorAbatimentoConcedido) / 100;
                detalhe.Descontos = Utils.ToDecimal(reg.ValorDescontoConcedido) / 100;
                detalhe.JurosMora = Utils.ToDecimal(reg.ValorJurosMora) / 100;

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }

        /*internal int ObtainNumeroDocumento()
        {
            int numeroDocumento = this.numeroDocumento + 1;
            this.numeroDocumento = numeroDocumento;

            return numeroDocumento;
        }*/

        public string GerarDetalheMultaRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            throw new NotImplementedException();
        }
    }
}

