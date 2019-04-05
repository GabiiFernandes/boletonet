using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoletoNet {

    public enum EnumInstrucoes_Cecred {
        /*
        * Código de Movimento Remessa 
        * 01 - Registro de títulos;  
        * 02 - Solicitação de baixa; 
        * 04 - Concessão de abatimento;  
        * 05 - Cancelamento de abatimento;  
        * 06 - Alteração de vencimento de título;  
        * 09 - Instruções para protestar (Nota 09);   
        * 10 - Instrução para sustar protesto;  
        * 12 - Alteração de nome e endereço do Pagador;  
        * 17 – Liquidação de título não registro ou pagamento em duplicidade; 
        * 31 - Conceder desconto; 
        * 32 - Não conceder desconto. 
        */
        CadastroDeTitulo = 1,
        PedidoBaixa = 2,
        ConcessaoAbatimento = 4,
        CancelamentoAbatimentoConcedido = 5,
        AlteracaoVencimento = 6,
        PedidoProtesto = 9,
        SustarProtestoBaixarTitulo = 10,
        AlteracaoNomeEnderecoPagador = 12,
        LiquidacaoDeTituloNaoRegristroOuPagamentoEmDuplicidade = 17,
        ConcederDesconto = 31,
        NaoConcederDesconto = 32
    }

    public class Instrucao_Cecred : AbstractInstrucao, IInstrucao {

        public Instrucao_Cecred() {
            try {
                this.Banco = new Banco_Cecred();
            } catch (Exception ex) {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public Instrucao_Cecred(int codigo) {
            try {

                this.Banco = new Banco_Cecred();
                
                switch ((EnumInstrucoes_Cecred)codigo)
                {
                    case EnumInstrucoes_Cecred.CadastroDeTitulo:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Cadastro De Título";
                        break;
                    case EnumInstrucoes_Cecred.PedidoBaixa:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Pedido de Baixa";
                        break;
                    case EnumInstrucoes_Cecred.ConcessaoAbatimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Concessão de Abatimento";
                        break;
                    case EnumInstrucoes_Cecred.CancelamentoAbatimentoConcedido:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Cancelamento Abatimento Concedido";
                        break;
                    case EnumInstrucoes_Cecred.AlteracaoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Alteração Vencimento";
                        break;
                    case EnumInstrucoes_Cecred.PedidoProtesto:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Pedido Protesto";
                        break;
                    case EnumInstrucoes_Cecred.SustarProtestoBaixarTitulo:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Sustar Protesto / Baixar Título";
                        break;
                    case EnumInstrucoes_Cecred.AlteracaoNomeEnderecoPagador:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Alteração Nome Endereço Pagador";
                        break;
                    case EnumInstrucoes_Cecred.LiquidacaoDeTituloNaoRegristroOuPagamentoEmDuplicidade:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Liquidação De Título Não Regristro Ou Pagamento Em Duplicidade";
                        break;
                    case EnumInstrucoes_Cecred.ConcederDesconto:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Conceder Desconto";
                        break;
                    case EnumInstrucoes_Cecred.NaoConcederDesconto:
                        this.Codigo = (int)EnumInstrucoes_Bradesco.Protestar;
                        this.Descricao = "Não Conceder Desconto";
                        break;
                }
            } catch (Exception ex) {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }
    }
}
