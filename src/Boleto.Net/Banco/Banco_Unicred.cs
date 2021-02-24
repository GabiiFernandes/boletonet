using System;
using System.Web.UI;
using BoletoNet;
using BoletoNet.Util;
using BoletoNet.EDI.Banco;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

[assembly: WebResource("BoletoNet.Imagens.422.jpg", "image/jpg")]

namespace BoletoNet
{
    //teste
    internal class Banco_Unicred : AbstractBanco, IBanco
    {
        private string _dacNossoNumero = string.Empty;
        private int _dacContaCorrente = 0;
        private int _dacBoleto = 0;
        private int sequencialArquivo = 1;

        internal Banco_Unicred()
        {
            this.Codigo = 136;
            this.Digito = "8";
            this.Nome = "Banco_Unicred";
        }

        internal int numeroDocumento = 0;

        public string CalcularDigitoNossoNumero(Boleto boleto)
        {
            if (boleto.NossoNumero.Length < 9)
            {
                throw new IndexOutOfRangeException("Erro. O campo 'Nosso Número' deve ter mais de 9 digitos. Você digitou " + boleto.NossoNumero);
            }
            string NossoNumero = boleto.NossoNumero.Substring(0, 10);
            int DigitoNossoNumero = Mod11(NossoNumero, 10, 1);
            DigitoNossoNumero = 11 - DigitoNossoNumero;
            if (DigitoNossoNumero > 9)
                DigitoNossoNumero = 0;
            string Digito = Convert.ToString(DigitoNossoNumero);
            return Digito;
        }

        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
        {
            try
            {
                string _header = ""; 

                base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Função não implementada: GerarHeaderRemessa() << CNAB240");
                    case TipoArquivo.CNAB400:
                        _header = GerarHeaderRemessaCNAB400(cedente, numeroArquivoRemessa);
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
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0012, 015, 0, "COBRANCA", ' '));                             //012-026
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0027, 020, 0, cedente.Codigo, '0')); //027-046
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0047, 030, 0, cedente.Nome, ' '));                           //047-076
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0077, 003, 0, 136, '0'));                                    //077-079
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0080, 015, 0, "UNICRED", ' '));                              //080-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0095, 006, 0, DateTime.Now, '0'));                           //095-100
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0101, 007, 0, string.Empty, ' '));                           //101-107
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0108, 003, 0, "000", '0'));                                  //108-110
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0111, 007, 0, numeroArquivoRemessa, '0'));                   //111-117
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0118, 277, 0, string.Empty, ' '));                           //118-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0395, 006, 0, this.sequencialArquivo, '0'));                               //395-400
                this.sequencialArquivo++;
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
                string _detalhe = "";
                string _detalhe7 = "";

                base.GerarDetalheRemessa(boleto, this.sequencialArquivo, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        throw new Exception("Função não implementada: GerarDetalheRemessa() << CNAB240");
                    case TipoArquivo.CNAB400:
                        _detalhe = GerarDetalheRemessaCNAB400(boleto, this.sequencialArquivo);
                        this.sequencialArquivo++;
                        _detalhe7 = GerarDetalhe7RemessaCNAB400(boleto, this.sequencialArquivo);
                        this.sequencialArquivo++;
                        break;
                    case TipoArquivo.Outro:
                         throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe + "\n" + _detalhe7;

            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", e);
            }
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            int instrucao1 = boleto.Instrucoes[boleto.Instrucoes.Count - 1].Codigo; //Considera sempre a última instrução pois o banco só aceita uma instrução para remessa
            int instrucaoProtesto = 0;
            int DiasProtesto = 0;

            if (instrucao1 == 27 || instrucao1 == 91)
            {
                instrucaoProtesto = 1;
                DiasProtesto = boleto.Instrucoes[boleto.Instrucoes.Count - 1].QuantidadeDias;
            }
            else if (instrucao1 == 92 || instrucao1 ==28)
            {
                instrucaoProtesto = 2;
                DiasProtesto = boleto.Instrucoes[boleto.Instrucoes.Count - 1].QuantidadeDias;
            }
            else if (instrucao1 == 90)
            {
                instrucaoProtesto = 3;
            }

            if (instrucao1 >= 90)
                instrucao1 = 9;
            else if (instrucao1 == 27 || instrucao1 == 28)
                instrucao1 = 26;

            try
            {
                /*
                * Código de Movimento Remessa
                *InstrucaoConfirmada = 02,
                *InstrucaoRejeitada = 03,
                */
                //string tipoInscricaoEmitente = "02";   // Padrão CNPJ
                string tipoInscricaoSacado = "00";
                if (boleto.Sacado.CPFCNPJ.Length == 14)
                {
                    tipoInscricaoSacado = "02";   // Padrão CNPJ
                }else if (boleto.Sacado.CPFCNPJ.Length == 11)
                {
                    tipoInscricaoSacado = "01";   // Padrão CPF
                }

                    string CodMora = boleto.PercJurosMora > 0 ? boleto.CodJurosMora == "1" ? "4" : "2" : "5";

                TRegistroEDI reg = new TRegistroEDI();

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "1", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0002, 005, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //002-006
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0007, 001, 0, "0", '0'));                                       //007-007
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0008, 012, 0, boleto.Cedente.ContaBancaria.Conta, '0'));        //008-019
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0020, 001, 0, boleto.Cedente.ContaBancaria.DigitoConta, '0'));  //020-020
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0021, 001, 0, "0", '0'));                                       //021-021
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0022, 003, 0, boleto.Cedente.Carteira, '0'));                   //022-024
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0025, 013, 0, "0", '0'));                                       //025-037
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, boleto.NumeroControle, ' '));                     //038-062
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 003, 0, "136", '0'));                                     //063-065
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0066, 002, 0, "00", '0'));                                      //066-067
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0068, 025, 0, string.Empty, ' '));                              //068-092
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0093, 001, 0, "0", '0'));                                       //093-093
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0094, 001, 0, boleto.PercMulta > 0 ? "2" : "3", '0'));          //094-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0095, 010, 2, boleto.PercMulta, '0'));                          //095-104
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0105, 001, 0, CodMora, '0'));                                   //105-105
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0106, 001, 0, "N", '0'));                                       //106-106
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0107, 002, 0, string.Empty, ' '));                              //107-108
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediInteiro______________, 0109, 002, 0, instrucao1, '0'));                                //109-110
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0111, 010, 0, boleto.NumeroDocumento, '0'));                    //111-120
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0121, 006, 0, boleto.DataVencimento, '0'));                     //121-126
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0127, 013, 2, boleto.ValorBoleto, '0'));                        //127-139
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0140, 010, 0, "0", '0'));                                       //140-149
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0150, 001, 0, boleto.ValorDesconto > 0 ? "1" : "0", '0'));      //150-150
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0151, 006, 0, boleto.DataDocumento, '0'));                      //151-156
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0157, 001, 0, "0", '0'));                                       //157-157
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0158, 001, 0, instrucaoProtesto, '0'));                         //158-158
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0159, 002, 0, DiasProtesto, '0'));                                       //159-160 
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0161, 013, 2, boleto.PercJurosMora, '0'));                      //161-173
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0174, 006, 0, boleto.ValorDesconto > 0 ? boleto.DataDesconto : DateTime.MinValue, '0')); //174-179
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0180, 013, 2, boleto.ValorDesconto, '0'));                      //180-192
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0193, 011, 0, boleto.NossoNumero + CalcularDigitoNossoNumero(boleto), '0'));      //193-203
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0204, 002, 0, "00", '0'));                                      //204-205
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0206, 013, 2, instrucao1 == 4 ? boleto.Abatimento : 0, '0'));  //206-218
                //PAGADOR
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0219, 002, 0, tipoInscricaoSacado, '0'));                       //219-220
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0221, 014, 0, boleto.Sacado.CPFCNPJ, '0'));                     //221-234
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0235, 040, 0, boleto.Sacado.Nome, ' '));                        //235-274
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0275, 040, 0, boleto.Sacado.Endereco.EndComNumero, ' '));       //275-314
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0315, 012, 0, boleto.Sacado.Endereco.Bairro, ' '));             //315-326
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0327, 008, 0, boleto.Sacado.Endereco.CEP, ' '));                //327-334
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0335, 020, 0, boleto.Sacado.Endereco.Cidade, '0'));             //335-354
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0355, 002, 0, boleto.Sacado.Endereco.UF, ' '));                 //355-356
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0357, 038, 0, string.Empty, ' '));                              //357-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0395, 006, 0, this.sequencialArquivo, '0'));                    //395-400
                
                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", e);
            }
        }


        #region 
        public string GerarDetalhe5RemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            try
            {
                /*
                * Código de Movimento Remessa
                *InstrucaoConfirmada = 02,
                *InstrucaoRejeitada = 03,
                */

                TRegistroEDI reg = new TRegistroEDI();

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "5", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 120, 0, string.Empty, ' '));                              //002-121
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0122, 002, 0, string.Empty, ' '));                              //122-123
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0124, 014, 0, "0", '0'));                                       //124-137
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0138, 040, 0, string.Empty, ' '));                              //138-177
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0178, 012, 0, string.Empty, ' '));                              //021-021
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0190, 008, 0, "0", '0'));                                       //022-024
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0198, 015, 0, "0", '0'));                                       //025-037
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0213, 002, 0, "00", '0'));                                      //038-062
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0215, 060, 0, string.Empty, ' '));                              //063-065
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0275, 060, 0, string.Empty, '0'));                              //066-067
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0335, 060, 0, "0", '0'));                                       //068-092
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0395, 006, 0, this.sequencialArquivo, '0'));                    //093-093

                reg.CodificarLinha();

                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE tipo 5 do arquivo CNAB400.", e);
            }
        }
        #endregion

        public string GerarDetalhe7RemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            var mensagens = new List<string>();

            foreach (IInstrucao instrucao in boleto.Instrucoes)
                if (instrucao.Codigo != 90)
                    mensagens.Add(instrucao.Descricao);
           
            int numeroMensagens = mensagens.Count;
            
            try
            {
                /*
                * Código de Movimento Remessa
                *InstrucaoConfirmada = 02,
                *InstrucaoRejeitada = 03,
                */

                TRegistroEDI reg = new TRegistroEDI();

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "7", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 003, 0, string.Empty, ' '));                              //002-004
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0005, 002, 0, "00", '0'));                                      //005-006
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0007, 080, 0, string.Empty, ' '));                              //007-086
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0087, 008, 0, string.Empty, ' '));                              //087-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0095, 040, 0, numeroMensagens >= 1 ? mensagens[0]: " ", ' '));   //095-134
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0135, 002, 0, "00", '0'));                                      //135-136
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0137, 080, 0, string.Empty, ' '));                              //137-216
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0217, 008, 0, string.Empty, ' '));                              //217-224
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0225, 040, 0, numeroMensagens >= 2 ? mensagens[1] : " ", ' '));  //225-264
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0265, 002, 0, "00", '0'));                                      //265-266
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0267, 080, 0, string.Empty, ' '));                              //267-346
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0347, 007, 0, string.Empty, ' '));                              //347-353
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0354, 040, 0, numeroMensagens >= 3 ? mensagens[2] : " ", ' '));  //354-393
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0394, 001, 0, string.Empty, ' '));                              //394-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0395, 006, 0, this.sequencialArquivo, '0'));                    //395-400

                reg.CodificarLinha();


                if (!string.IsNullOrEmpty(reg.LinhaRegistro))
                {
                    var sb = new StringBuilder();
                    var arrayChar = reg.LinhaRegistro.Normalize(NormalizationForm.FormD).ToCharArray();

                    foreach (char c in arrayChar)
                    {
                        if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            sb.Append(c);
                    }
                    return Regex.Replace(sb.ToString(), @"[^0-9a-zA-Z\%\=\/\$°ºª&¹²³.,\\@\- ]+", " ")
                        .Replace("ª", "a")
                        .Replace("º", "o")
                        .Replace("°", "o")
                        .Replace("&", "e")
                        .Replace("¹", "1")
                        .Replace("²", "2")
                        .Replace("³", "3");
                }
                return Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE tipo 7 do arquivo CNAB400.", e);
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
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 393, 0, string.Empty, ' '));                  //002-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliDireita______, 0395, 006, 0, this.sequencialArquivo, '0'));                      //395-400

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
                TRegistroEDI_Unicred_Retorno reg = new TRegistroEDI_Unicred_Retorno();
                reg.LinhaRegistro = registro;
                reg.DecodificarLinha();

                //Passa para o detalhe as propriedades de reg;
                DetalheRetorno detalhe = new DetalheRetorno(registro);

                detalhe.Agencia = Utils.ToInt32(String.Concat(reg.Agencia, reg.DigitoAgencia));
                detalhe.Conta = Utils.ToInt32(reg.Conta);
                detalhe.DACConta = Utils.ToInt32(reg.DigitoConta);
                detalhe.Carteira = "21";                
                detalhe.CodigoOcorrencia = Utils.ToInt32(reg.TipoOcorrencia);
                detalhe.DescricaoOcorrencia = new CodigoMovimento_Unicred(detalhe.CodigoOcorrencia, Utils.ToInt32(reg.ComplementoMovimento).ToString()).Descricao;
                int dataLiquidacao = Utils.ToInt32(reg.DataLiquidacao);
                detalhe.DataLiquidacao = Utils.ToDateTime(dataLiquidacao.ToString("##-##-##"));
                detalhe.NumeroDocumento = reg.SeuNumero;
                int dataVencimento = Utils.ToInt32(reg.VencimentoTitulo);
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                detalhe.ValorTitulo = Utils.ToDecimal(reg.ValorNominalTitulo) / 100;
                detalhe.CodigoBanco = Utils.ToInt32(reg.CodigoBanco);
                detalhe.AgenciaCobradora = Utils.ToInt32(reg.Agencia);
                detalhe.IOF = Utils.ToDecimal(reg.ValorTarifas) / 100;
                detalhe.Abatimentos = Utils.ToDecimal(reg.ValorAbatimento) / 100;
                detalhe.Descontos = Utils.ToDecimal(reg.ValorDescontoConcedido) / 100;
                detalhe.JurosMora = Utils.ToDecimal(reg.ValorJurosMora) / 100;

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }

        public override void ValidaBoleto(Boleto boleto)
        {
            // Calcula o DAC da Conta Corrente
            _dacContaCorrente = Mod10(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta);

            //Verifica se o nosso número é válido
            if (Utils.ToInt64(boleto.NossoNumero) == 0)
                throw new NotImplementedException("Nosso número inválido");

            //Verifica se data do processamento é valida
            //if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
            if (boleto.DataProcessamento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataProcessamento = DateTime.Now;

            //Verifica se data do documento é valida
            //if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
            if (boleto.DataDocumento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataDocumento = DateTime.Now;

            boleto.Aceite = "Não";
            boleto.LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA/CORRESPONDENTE BANCÁRIO";
            boleto.DigitoNossoNumero = _dacNossoNumero;

            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }

        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            string Livre = CampoLivre(boleto);
 
            int[] digitosVerificadores;
            int asciiZero = 48;
            boleto.CodigoBarra.LinhaDigitavel = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
            Codigo, boleto.Moeda, Livre.Substring(0, 5), 'X', Livre.Substring(5, 10), 'Y', Livre.Substring(15,10), 'Z', this._dacBoleto, FatorVencimento(boleto), GetValorBoletoFormatado(boleto.ValorBoleto));

            digitosVerificadores = GetDigitosVerificadoresLinhaDigitavel(boleto.CodigoBarra.LinhaDigitavel);
            boleto.CodigoBarra.LinhaDigitavel = boleto.CodigoBarra.LinhaDigitavel
                .Replace('X', Convert.ToChar(asciiZero + digitosVerificadores[0]))
                .Replace('Y', Convert.ToChar(asciiZero + digitosVerificadores[1]))
                .Replace('Z', Convert.ToChar(asciiZero + digitosVerificadores[2]));
            boleto.CodigoBarra.LinhaDigitavel = Regex.Replace(boleto.CodigoBarra.LinhaDigitavel, "(.{5})(.{5})(.{5})(.{6})(.{5})(.{6})(.)(.{14})",
            "$1.$2 $3.$4 $5.$6 $7 $8");
        }

        public string CampoLivre(Boleto boleto)
        {
            return FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Agencia.ToString(),4)
                + FormatZerosEsquerda(boleto.Cedente.ContaBancaria.Conta.ToString() + boleto.Cedente.ContaBancaria.DigitoConta, 10) 
                + FormatZerosEsquerda(boleto.NossoNumero + this._dacNossoNumero, 11);
        }

        public override void FormataCodigoBarra(Boleto boleto)
        {
            boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}",
            Codigo, boleto.Moeda, 'D', FatorVencimento(boleto), GetValorBoletoFormatado(boleto.ValorBoleto), CampoLivre(boleto));
            _dacBoleto = GetDacBoleto(boleto.CodigoBarra.Codigo);
            boleto.CodigoBarra.Codigo = boleto.CodigoBarra.Codigo.Replace("D", _dacBoleto.ToString());
        }

        public override void FormataNossoNumero(Boleto boleto)
        {
            // Calcula o DAC do Nosso Número
            _dacNossoNumero = CalcularDigitoNossoNumero(boleto);
            boleto.NossoNumero += '-' + this._dacNossoNumero;
        }

        public override void FormataNumeroDocumento(Boleto boleto)
        {
        }
        
        public override bool ValidarRemessa(TipoArquivo tipoArquivo, string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, int numeroArquivoRemessa, out string mensagem)
        {
            bool vRetorno = true;
            string vMsg = string.Empty;
            ////IMPLEMENTACAO PENDENTE...
            mensagem = vMsg;
            return vRetorno;
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
                    aux = ((int)Char.GetNumericValue(digito) * 2);
                }
                else
                {
                    aux = ((int)Char.GetNumericValue(digito) * 1);
                }
                digitosMultSum += (aux >= 10) ? aux - 9 : aux;
                even = !even;
            }

            divisao = (digitosMultSum % 10);
            return (divisao % 10 == 0) ? 0 : (10 - divisao);
        }

        internal string GetValorBoletoFormatado(decimal valor)
        {
            string valorBoleto = valor.ToString("f").Replace(",", "").Replace(".", "");
            return FormatZerosEsquerda(valorBoleto, 10);
        }

        internal string FormatZerosEsquerda(string texto, int tamanhoMax)
        {
            string resultado = "";
            for (int i = texto.Length; i < tamanhoMax; i++)
            {
                resultado += "0";
            }
            resultado += texto;
            return resultado;
        }

        public string GerarDetalheMultaRemessaCNAB400(Boleto boleto, int numeroRegistro)
        {
            throw new NotImplementedException();
        }
    }
}