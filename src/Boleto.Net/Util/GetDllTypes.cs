using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;

namespace BoletoNet
{
    public class ComandoRemessa
    {

        public int Codigo { get; set; }
        public string Descricao { get; set; }
     
    }


    //[Serializable, Browsable(false)]
    public class GetDllTypes
    {
        public GetDllTypes() { }

        #region TipoArquivo
        public TipoArquivo getTipoArquivo(String StringTipoArquivo)
        {
            switch (StringTipoArquivo)
            {
                case "CNAB240"  : return TipoArquivo.CNAB240;
                case "CNAB400"  : return TipoArquivo.CNAB400;
                case "CBR643"   : return TipoArquivo.CBR643;
                case "Outro"    : return TipoArquivo.Outro;
                default         : return TipoArquivo.CNAB240;
            }
        }
        #endregion

        #region Banco

        public Banco newBanco(int codigoBanco)
        {
            return new Banco(codigoBanco);
        }

        #endregion

        #region Instruções

        public Instrucao newInstrucao(int codigoBanco)
        {
            return new Instrucao(codigoBanco);
        }
        public Instrucoes getInstrucoes(int codigoBanco)
        {
            Type enumInstucao_banco = null;

            try
            {
                switch (codigoBanco)
                {
                    //399 - HSBC
                    case 399:
                        enumInstucao_banco = typeof(EnumInstrucoes_HSBC);
                        break;
                    //104 - Caixa
                    case 104:
                        enumInstucao_banco = typeof(EnumInstrucoes_Caixa);
                        break;
                    case 136:
                        enumInstucao_banco = typeof(EnumInstrucoes_Unicred1);
                        break;
                    //341 - Itaú
                    case 341:
                        enumInstucao_banco = typeof(EnumInstrucoes_Itau);
                        break;
                    //1 - Banco do Brasil
                    case 1:
                        enumInstucao_banco = typeof(EnumInstrucoes_BancoBrasil);
                        break;
                    //356 - Real
                    case 356:
                        enumInstucao_banco = typeof(EnumInstrucoes_Real);
                        break;
                    //422 - Safra
                    case 422:
                        enumInstucao_banco = typeof(EnumInstrucoes_Safra1);
                        break;
                    //237 - Bradesco
                    //707 - Daycoval
                    case 237:
                    case 707:
                        enumInstucao_banco = typeof(EnumInstrucoes_Bradesco);
                        break;
                    //347 - Sudameris
                    case 347:
                        enumInstucao_banco = typeof(EnumInstrucoes_Sudameris);
                        break;
                    //353 - Santander
                    case 353:
                    case 33:
                    case 8:
                        //case 8:
                        enumInstucao_banco = typeof(EnumInstrucoes_Santander);
                        break;
                    //070 - BRB
                    case 70:
                        enumInstucao_banco = typeof(EnumInstrucoes_BRB);
                        break;
                    //479 - BankBoston
                    case 479:
                        enumInstucao_banco = typeof(EnumInstrucoes_BankBoston);
                        break;
                    //41 - Banrisul
                    case 41:
                        enumInstucao_banco = typeof(EnumInstrucoes_Banrisul);
                        break;
                    //756 - Sicoob
                    case 756:
                        enumInstucao_banco = typeof(EnumInstrucoes_Sicoob);
                        break;
                    //85 - CECRED
                    case 85:
                        enumInstucao_banco = typeof(EnumInstrucoes_Cecred);
                        break;
                    //748 - Sicredi
                    case 748:
                        enumInstucao_banco = typeof(EnumInstrucoes_Sicredi);
                        break;
                    //655 - Votorantim
                    case 655:
                        enumInstucao_banco = typeof(EnumInstrucoes_Votorantim);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Código do Banco Implementado.", ex);
            }

            Instrucoes instrucoes = new Instrucoes();

            if (enumInstucao_banco != null)
                foreach (int value in Enum.GetValues(enumInstucao_banco))
                {
                    try
                    {
                        Instrucao instrucao = new Instrucao(codigoBanco);

                        instrucao.Codigo = value;
                        instrucao.Descricao = Enum.GetName(enumInstucao_banco, value);

                        instrucoes.Add(instrucao);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Não foi possível carregar Instrução", e);
                    }
                }

            return instrucoes;
        }
        public String getInstrucoesJson(int codigoBanco)
        {
            return JsonConvert.SerializeObject(getInstrucoes(codigoBanco));
        }
        public Instrucao CarregaInstrucao(int codigoBanco, int codigoInstrucao, int quantidadeDias)
        {
            IInstrucao _IInstrucao = null;
            //Instrucao instrucao;
            try
            {
                switch (codigoBanco)
                {
                    //399 - HSBC
                    case 399:
                        _IInstrucao = new Instrucao_HSBC(codigoInstrucao, quantidadeDias);
                        break;
                    //104 - Caixa
                    case 104:
                        _IInstrucao = new Instrucao_Caixa(codigoInstrucao, quantidadeDias);
                        break;
                    //341 - Itaú
                    case 341:
                        _IInstrucao = new Instrucao_Itau(codigoInstrucao, quantidadeDias);
                        break;
                    //1 - Banco do Brasil
                    case 1:
                        _IInstrucao = new Instrucao_BancoBrasil(codigoInstrucao, quantidadeDias);
                        break;
                    //356 - Real
                    case 356:
                        _IInstrucao = new Instrucao_Real(codigoInstrucao, quantidadeDias);
                        break;
                    //422 - Safra
                    case 422:
                        _IInstrucao = new Instrucao_Safra(codigoInstrucao, quantidadeDias);
                        break;
                    //237 - Bradesco
                    //707 - Daycoval
                    case 237:
                    case 707:
                        _IInstrucao = new Instrucao_Bradesco(codigoInstrucao, quantidadeDias);
                        break;
                    //347 - Sudameris
                    case 347:
                        _IInstrucao = new Instrucao_Sudameris(codigoInstrucao, quantidadeDias);
                        break;
                    //353 - Santander
                    case 353:
                    case 33:
                    case 8:
                        //case 8:
                        _IInstrucao = new Instrucao_Santander(codigoInstrucao, quantidadeDias);
                        break;
                    //070 - BRB
                    case 70:
                        _IInstrucao = new Instrucao_BRB(codigoInstrucao, quantidadeDias);
                        break;
                    //479 - BankBoston
                    case 479:
                        _IInstrucao = new Instrucao_BankBoston(codigoInstrucao, quantidadeDias);
                        break;
                    //41 - Banrisul
                    case 41:
                        _IInstrucao = new Instrucao_Banrisul(codigoInstrucao, quantidadeDias);
                        break;
                    //756 - Sicoob
                    case 756:
                        _IInstrucao = new Instrucao_Sicoob(codigoInstrucao, quantidadeDias);
                        break;
                    //85 - CECRED
                    case 85:
                        _IInstrucao = new Instrucao_Cecred(codigoInstrucao, quantidadeDias);
                        break;
                    //748 - Sicredi
                    case 748:
                        _IInstrucao = new Instrucao_Sicredi(codigoInstrucao, quantidadeDias);
                        break;
                    //655 - Votorantim
                    case 655:
                        //_IInstrucao = new Instrucao_Votorantim();
                        break;
                    case 136:
                        _IInstrucao = new Instrucao_Unicred(codigoInstrucao, quantidadeDias);
                        break;
                    default:
                        throw new Exception("Código do banco não implementando: " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
            Instrucao instrucao = new Instrucao(codigoBanco);
            instrucao.Codigo = codigoInstrucao;
            instrucao.Descricao = _IInstrucao.Descricao;
            instrucao.QuantidadeDias = _IInstrucao.QuantidadeDias;
            
            return instrucao;
        }

        #endregion

        #region Comandos Remessa

        public List<ComandoRemessa> getComandosRemessa()
        {
            // Instrucoes instrucoes = new Instrucoes();
            List<ComandoRemessa> comandosRemessa = new List<ComandoRemessa>();

            foreach (int value in Enum.GetValues(typeof(TipoOcorrenciaRemessa)))
            {
                ComandoRemessa comandoRemessa = new ComandoRemessa();
                comandoRemessa.Codigo = value;

                switch (value)
                {
                    case 1:
                        comandoRemessa.Descricao = "Entrada de Títulos";
                        break;
                    case 2:
                        comandoRemessa.Descricao = "Pedido de Baixa";
                        break;
                    case 3:
                        comandoRemessa.Descricao = "Protesto para Fins Falimentares";
                        break;
                    case 4:
                        comandoRemessa.Descricao = "Concessão de Abatimento";
                        break;
                    case 5:
                        comandoRemessa.Descricao = "Cancelamento de Abatimento";
                        break;
                    case 6:
                        comandoRemessa.Descricao = "Alteração de Vencimento";
                        break;
                    case 7:
                        comandoRemessa.Descricao = "Concessão de Desconto";
                        break;
                    case 8:
                        comandoRemessa.Descricao = "Cancelamento de Desconto";
                        break;
                    case 9:
                        comandoRemessa.Descricao = "Protestar";
                        break;
                    case 10:
                        comandoRemessa.Descricao = "Sustar Protesto e Baixar Título";
                        break;
                    case 11:
                        comandoRemessa.Descricao = "Sustar Protesto e Manter em Carteira";
                        break;
                    case 12:
                        comandoRemessa.Descricao = "Alteração de Juros de Mora";
                        break;
                    case 13:
                        comandoRemessa.Descricao = "Dispensar Cobrança de Juros de Mora";
                        break;
                    case 14:
                        comandoRemessa.Descricao = "Alteração de Valor/Percentual de Multa";
                        break;
                    case 15:
                        comandoRemessa.Descricao = "Dispensar Cobrança de Multa";
                        break;
                    case 16:
                        comandoRemessa.Descricao = "Alteração de Valor/Data de Desconto";
                        break;
                    case 17:
                        comandoRemessa.Descricao = "Não conceder Desconto";
                        break;
                    case 18:
                        comandoRemessa.Descricao = "Alteração do Valor de Abatimento";
                        break;
                    case 19:
                        comandoRemessa.Descricao = "Prazo Limite de Recebimento - Alterar";
                        break;
                    case 20:
                        comandoRemessa.Descricao = "Prazo Limite de Recebimento - Dispensar";
                        break;
                    case 21:
                        comandoRemessa.Descricao = "Alterar número do título dado pelo Beneficiário";
                        break;
                    case 22:
                        comandoRemessa.Descricao = "Alterar número controle do Participante";
                        break;
                    case 23:
                        comandoRemessa.Descricao = "Alterar dados do Pagador";
                        break;
                    case 24:
                        comandoRemessa.Descricao = "Alterar dados do Sacador/Avalista";
                        break;
                    case 30:
                        comandoRemessa.Descricao = "Recusa da Alegação do Pagador";
                        break;
                    case 31:
                        comandoRemessa.Descricao = "Alteração de Outros Dados";
                        break;
                    case 33:
                        comandoRemessa.Descricao = "Alteração dos Dados do Rateio de Crédito";
                        break;
                    case 34:
                        comandoRemessa.Descricao = "Pedido de Cancelamento dos Dados do Rateio de Crédito";
                        break;
                    case 35:
                        comandoRemessa.Descricao = "Pedido de Desagendamento do Débito Automático";
                        break;
                    case 40:
                        comandoRemessa.Descricao = "Alteração de Carteira";
                        break;
                    case 41:
                        comandoRemessa.Descricao = "Cancelar protesto";
                        break;
                    case 42:
                        comandoRemessa.Descricao = "Alteração de Espécie de Título";
                        break;
                    case 43:
                        comandoRemessa.Descricao = "Transferência de carteira/modalidade de cobrança";
                        break;
                    case 44:
                        comandoRemessa.Descricao = "Alteração de contrato de cobrança";
                        break;
                    case 45:
                        comandoRemessa.Descricao = "Negativação Sem Protesto";
                        break;
                    case 46:
                        comandoRemessa.Descricao = "Solicitação de Baixa de Título Negativado Sem Protesto";
                        break;
                    case 47:
                        comandoRemessa.Descricao = "Alteração do Valor Nominal do Título";
                        break;
                }

                comandosRemessa.Add(comandoRemessa);
           
            }
       
            return comandosRemessa;
        }
        public String getComandosRemessaJson()
        {
            return JsonConvert.SerializeObject(getComandosRemessa());
        }

        #endregion

        #region Comandos Retorno

        public List<CodigoMovimento> getComandosRetorno(int codigoBanco)
        {
            Type enumComandosRetorno = null;

            try
            {
                switch (codigoBanco)
                {
                    case 33:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Santander);
                        break;
                    // Caixa
                    case 104:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Caixa);
                        break;
                    //136 - Unicred
                    case 136:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Unicred);
                        break;
                    //341 - Itaú
                    case 341:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Itau);
                        break;
                    //1 - Banco do Brasil
                    case 1:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_BancoBrasil);
                        break;
                    //Bradesco
                    case 237:
                        codigoBanco = 85; //utiliza codigos de movimento da cecred poís são iguais
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Cecred);
                        break;
                    case 353:
                    case 422: //Safra
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Safra);
                        break;
                    case 756:
                    // 85 - CECRED
                    case 85:
                        codigoBanco = 85; //Modifica o parametro de entrada para que os códigos de movimento sejam instanciados pelo cecred
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Cecred);
                        break;
                    case 748:
                        enumComandosRetorno = typeof(EnumCodigoMovimento_Sicredi);
                        break;
                        // default:
                        // throw new Exception("Código do banco não implementando: " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Código do Banco Implementado. Testes " + codigoBanco, ex);
            }

            // Instrucoes instrucoes = new Instrucoes();
            List<CodigoMovimento> codigosMovimento = new List<CodigoMovimento>();

            if (enumComandosRetorno != null)
                foreach (int value in Enum.GetValues(enumComandosRetorno))
                {
                    try
                    {
                        CodigoMovimento codMovimento = new CodigoMovimento(codigoBanco, value);

                        codigosMovimento.Add(codMovimento);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Não foi possível carregar Instrução", e);
                    }
                }

            return codigosMovimento;
        }
        public String getComandosRetornoJson(int codigoBanco)
        {
            return JsonConvert.SerializeObject(getComandosRetorno(codigoBanco));
        }

        #endregion

        #region Retorno

        public IBanco getBancoInterface(Banco banco)
        {
            return banco;
        }

        public Stream getStreamReader(string caminhoArquivo)
        {
            return new StreamReader(caminhoArquivo).BaseStream;
        }

        #endregion
    }
}
