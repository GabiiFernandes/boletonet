using BoletoNet.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet
{
    public enum EnumCodigoMovimento_Unicred
    {
        InstrucaoConfirmada = 02,
        InstrucaoRejeitada = 03,
        Liquidacao = 06,
    }
    public class CodigoMovimento_Unicred : AbstractCodigoMovimento, ICodigoMovimento
    {

		internal CodigoMovimento_Unicred()
        {
        }
    
        public CodigoMovimento_Unicred(int codigo)
        {
            try
            {
				this.Banco = new Banco_Unicred();
				Codigo = codigo;
				Descricao = CarregaDescricao(codigo);
			}
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public CodigoMovimento_Unicred(int codigo, string codComplemento)
        {
            try
            {
				this.Banco = new Banco_Unicred();
				Codigo = codigo;
				Descricao = CarregaComplemento(codComplemento);
			}
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }
        private string CarregaDescricao(int codigo)
        {
            try
            {
				switch(codigo)
                {
					case 2:
						return "Entrada confirmada";
					case 3:
						return "Entrada Rejeitada";
					case 6:
						return "Liquidação";
					default:
						return "";
                }


			}catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação.", ex);
			}
		}
        private string CarregaComplemento(string codComplemento)
        {
            try
            {
                switch (codComplemento)
                {
					case "00":
						return "Sem Complemento a informar";
					case "01":
						return "Código do Banco Inválido";
					case "04":
						return "Código de Movimento não permitido para a carteira";
					case "05":
						return "Código de Movimento Inválido";
					case "06":
						return "Número de Inscrição do Beneficiário Inválido";
					case "07":
						return "Agência - Conta Inválida";
					case "08":
						return "Nosso Número Inválido";
					case "09":
						return "Nosso Número Duplicado";
					case "10":
						return "Carteira inválida";
					case "12":
						return "Tipo de Documento Inválido";
					case "15":
						return "Data de Vencimento inferior a 5 dias uteis para remessa gráfica";
					case "16":
						return "Data de Vencimento Inválida";
					case "17":
						return "Data de Vencimento Anterior à Data de Emissão";
					case "18":
						return "Vencimento fora do Prazo de Operação";
					case "20":
						return "Valor do Título Inválido";
					case "24":
						return "Data de Emissão Inválida";
					case "25":
						return "Data de Emissão Posterior à data de Entrega";
					case "26":
						return "Código de juros inválido";
					case "27":
						return "Valor de juros inválido";
					case "28":
						return "Código de Desconto inválido";
					case "29":
						return "Valor de Desconto inválido";
					case "30":
						return "Alteração de Dados Rejeitada";
					case "33":
						return "Valor de Abatimento Inválido";
					case "34":
						return "Valor do Abatimento Maior ou Igual ao Valor do título";
					case "37":
						return "Código para Protesto Inválido; (Protesto via SGR, não é CRA)";
					case "38":
						return "Prazo para Protesto Inválido; (Protesto via SGR, não é CRA)";
					case "39":
						return "Pedido de Protesto Não Permitido para o Título";
					case "40":
						return "Título com Ordem de Protesto Emitida";
					case "41":
						return "Pedido de Cancelamento/Sustação para Títulos sem Instrução de Protesto";
					case "45":
						return "Nome do Pagador não informado";
					case "46":
						return "Número de Inscrição do Pagador Inválido";
					case "47":
						return "Endereço do Pagador Não Informado";
					case "48":
						return "CEP Inválido";
					case "52":
						return "Unidade Federativa Inválida";
					case "57":
						return "Código de Multa inválido";
					case "58":
						return "Data de Multa inválido";
					case "59":
						return "Valor / percentual de Multa inválido";
					case "60":
						return "Movimento para Título não Cadastrado";
					case "63":
						return "Entrada para Título já cadastrado";
					case "79":
						return "Data de Juros inválida";
					case "80":
						return "Data de Desconto inválida";
					case "86":
						return "Seu Número Inválido";
					case "A5":
						return "Título Liquidado";
					case "A8":
						return "Valor do Abatimento Inválido para Cancelamento";
					case "C0":
						return "Sistema Intermitente – Entre em contato com sua Cooperativa";
					case "C1":
						return "Situação do título Aberto";
					case "C3":
						return "Status do Borderô Inválido";
					case "C4":
						return "Nome do Beneficiário Inválido";
					case "C5":
						return "Documento Inválido";
					case "C6":
						return "Instrução não Atualiza Cadastro do Título";
					case "C7":
						return "Título não registrado na CIP";
					case "C8":
						return "Situação do Borderô inválida";
					case "C9":
						return "Título inválido conforme situação CIP";
					case "E0":
						return "CEP indicado para o endereço do Pagador não compatível com os Correios";
					case "E1":
						return "Logradouro para o endereço do Pagador não compatível com os Correios, para o CEP indicado";
					case "E2":
						return "Tipo de logradouro para o endereço do Pagador não compatível com os Correios, para o CEP indicado";
					case "E3":
						return "Bairro para o endereço do Pagador não compatível com os Correios, para o CEP indicado";
					case "E4":
						return "Cidade para o endereço do Pagador não compatível com os Correios, para o CEP indicado";
					case "E5":
						return "UF para o endereço do Pagador não compatível com os Correios, para o CEP indicado";
					case "E6":
						return "Dados do segmento/registro opcional de endereço do pagador, incompletos no arquivo remessa";
					case "E7":
						return "Beneficiário não autorizado a enviar boleto por e-mail";
					case "E8":
						return "Indicativo para pagador receber boleto por e-mail sinalizado, porém sem o endereço do e-mail";
					case "101":
						return "Data da apresentação inferior à data de vencimento";
					case "102":
						return "Falta de comprovante da prestação de serviço";
					case "103":
						return "Nome do sacado incompleto/incorreto";
					case "104":
						return "Nome do cedente incompleto/incorreto";
					case "105":
						return "Nome do sacador incompleto/incorreto";
					case "106":
						return "Endereço do sacado insuficiente";
					case "107":
						return "CNPJ/CPF do sacado inválido/incorreto";
					case "108":
						return "CNPJ/CPF incompatível c/ o nome do sacado/sacador/avalista";
					case "109":
						return "CNPJ/CPF do sacado incompatível com o tipo de documento";
					case "110":
						return "CNPJ/CPF do sacador incompatível com a espécie";
					case "111":
						return "Título aceito sem a assinatura do sacado";
					case "112":
						return "Título aceito rasurado ou rasgado";
					case "113":
						return "Título aceito – falta título (ag ced: enviar)";
					case "114":
						return "CEP incorreto";
					case "115":
						return "Praça de pagamento incompatível com endereço";
					case "116":
						return "Falta número do título";
					case "117":
						return "Título sem endosso do cedente ou irregular";
					case "118":
						return "Falta data de emissão do título";
					case "119":
						return "Título aceito: valor por extenso diferente do valor por numérico";
					case "120":
						return "Data de emissão posterior ao vencimento";
					case "121":
						return "Espécie inválida para protesto";
					case "122":
						return "CEP do sacado incompatível com a praça de protesto";
					case "123":
						return "Falta espécie do título";
					case "124":
						return "Saldo maior que o valor do título";
					case "125":
						return "Tipo de endosso inválido";
					case "126":
						return "Devolvido por ordem judicial";
					case "127":
						return "Dados do título não conferem com disquete";
					case "128":
						return "Sacado e Sacador/Avalista são a mesma pessoa";
					case "129":
						return "Corrigir a espécie do título";
					case "130":
						return "Aguardar um dia útil após o vencimento para protestar";
					case "131":
						return "Data do vencimento rasurada";
					case "132":
						return "Vencimento – extenso não confere com número";
					case "133":
						return "Falta data de vencimento no título";
					case "134":
						return "DM/DMI sem comprovante autenticado ou declaração";
					case "135":
						return "Comprovante ilegível para conferência e microfilmagem";
					case "136":
						return "Nome solicitado não confere com emitente ou sacado";
					case "137":
						return "Confirmar se são 2 emitentes. Se sim, indicar os dados dos 2";
					case "138":
						return "Endereço do sacado igual ao do sacador ou do portador";
					case "139":
						return "Endereço do apresentante incompleto ou não informado";
					case "140":
						return "Rua / Número inexistente no endereço";
					case "141":
						return "Informar a qualidade do endosso (M ou T)";
					case "142":
						return "Falta endosso do favorecido para o apresentante";
					case "143":
						return "Data da emissão rasurada";
					case "144":
						return "Protesto de cheque proibido – motivo 20/25/28/30 ou 35";
					case "145":
						return "Falta assinatura do emitente no cheque";
					case "146":
						return "Endereço do emitente no cheque igual ao do banco sacado";
					case "147":
						return "Falta o motivo da devolução no cheque ou motivo ilegível";
					case "148":
						return "Falta assinatura do sacador no título";
					case "149":
						return "Nome do apresentante não informado/incompleto/incorreto";
					case "150":
						return "Erro de preenchimento do título";
					case "151":
						return "Título com direito de regresso vencido";
					case "152":
						return "Título apresentado em duplicidade";
					case "153":
						return "Título já protestado";
					case "154":
						return "Letra de Câmbio vencida – falta aceite do sacado";
					case "155":
						return "Título – falta tradução por tradutor público";
					case "156":
						return "Falta declaração de saldo assinada no título";
					case "157":
						return "Contrato de Câmbio – falta conta gráfica";
					case "158":
						return "Ausência do Documento Físico";
					case "159":
						return "Sacado Falecido";
					case "160":
						return "Sacado Apresentou Quitação do Título";
					case "161":
						return "Título de outra jurisdição territorial";
					case "162":
						return "Título com emissão anterior à concordata do sacado";
					case "163":
						return "Sacado consta na lista de falência";
					case "164":
						return "Apresentante não aceita publicação de edital";
					case "165":
						return "Dados do sacador em branco ou inválido";
					case "166":
						return "Título sem autorização para protesto por edital";
					case "167":
						return "Valor divergente entre título e comprovante";
					case "168":
						return "Condomínio não pode ser protestado para fins falimentares";
					case "169":
						return "Vedada a intimação por edital para protesto falimentar";
					case "170":
						return "Dados do Cedente em branco ou inválido";
					default:
						return "Nenhum complemento encontrado";				}
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
        }


		private readonly Dictionary<EnumCodigoMovimento_Unicred, TipoOcorrenciaRetorno> correspondentesFebraban = new Dictionary<EnumCodigoMovimento_Unicred, TipoOcorrenciaRetorno>()
        {
            { EnumCodigoMovimento_Unicred.InstrucaoConfirmada, TipoOcorrenciaRetorno.EntradaConfirmada },
            { EnumCodigoMovimento_Unicred.InstrucaoRejeitada, TipoOcorrenciaRetorno.EntradaRejeitada },
			{ EnumCodigoMovimento_Unicred.Liquidacao, TipoOcorrenciaRetorno.Liquidacao}
        };

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            return ObterCorrespondenteFebraban(correspondentesFebraban, (EnumCodigoMovimento_Unicred)Codigo);
        }
    }
}
