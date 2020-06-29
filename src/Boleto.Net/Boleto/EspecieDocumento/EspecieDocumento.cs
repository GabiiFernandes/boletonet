using BoletoNet.Excecoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class EspecieDocumento : IEspecieDocumento
    {

        #region Variaveis

        private IEspecieDocumento _IEspecieDocumento;

        #endregion

        #region Construtores

        public EspecieDocumento()
        {
        }

        public EspecieDocumento(int CodigoBanco)
        {
            try
            {
                InstanciaEspecieDocumento(CodigoBanco, "0");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        public EspecieDocumento(int CodigoBanco, string codigoEspecie)
        {
            try
            {
                InstanciaEspecieDocumento(CodigoBanco, codigoEspecie);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        #endregion

        #region Propriedades da interface

        public IBanco Banco
        {
            get { return _IEspecieDocumento.Banco; }
            set { _IEspecieDocumento.Banco = value; }
        }

        public string Codigo
        {
            get { return _IEspecieDocumento.Codigo; }
            set { _IEspecieDocumento.Codigo = value; }
        }

        public string Sigla
        {
            get
            {

                if (_IEspecieDocumento == null)
                {
                    return string.Empty;
                }

                return _IEspecieDocumento.Sigla;
            }
            set { _IEspecieDocumento.Sigla = value; }
        }

        public string Especie
        {
            get { return _IEspecieDocumento.Especie; }
            set { _IEspecieDocumento.Especie = value; }
        }

        #endregion

        # region Métodos

        public void InstanciaEspecieDocumento(int codigoBanco, string codigoEspecie)
        {
            try
            {
                switch (codigoBanco)
                {
                    case 1:
                        _IEspecieDocumento = new EspecieDocumento_BancoBrasil(codigoEspecie);
                        break;
                    //004 Banco do Nordeste
                    case 4:
                        _IEspecieDocumento = new EspecieDocumento_Nordeste(codigoEspecie);
                        break;
                    //033 - Santander
                    case 33:
                        _IEspecieDocumento = new EspecieDocumento_Santander(codigoEspecie);
                        break;
                    //41 - Banrisul - sidneiklein
                    case 41:
                        _IEspecieDocumento = new EspecieDocumento_Banrisul(codigoEspecie);
                        break;
                    //085 - Cecred
                    case 85:
                        _IEspecieDocumento = new EspecieDocumento_Cecred(codigoEspecie);
                        break;
                    //104 - Caixa
                    case 104:
                        _IEspecieDocumento = new EspecieDocumento_Caixa(codigoEspecie);
                        break;
                    //237 - Bradesco
                    case 237:
                        _IEspecieDocumento = new EspecieDocumento_Bradesco(codigoEspecie);
                        break;
                    //341 - Itaú
                    case 341:
                        _IEspecieDocumento = new EspecieDocumento_Itau(codigoEspecie);
                        break;
                    case 347:
                        _IEspecieDocumento = new EspecieDocumento_Sudameris(codigoEspecie);
                        break;
                    case 356:
                        _IEspecieDocumento = new EspecieDocumento_Real(codigoEspecie);
                        break;
                    //399 - HSBC
                    case 399:
                        _IEspecieDocumento = new EspecieDocumento_HSBC(codigoEspecie);
                        break;
                    //422 - Safra
                    case 422:
                        _IEspecieDocumento = new EspecieDocumento_Safra(codigoEspecie);
                        break;
                    //479 - BankBoston
                    case 479:
                        _IEspecieDocumento = new EspecieDocumento_BankBoston(codigoEspecie);
                        break;
                    case 637:
                        this._IEspecieDocumento = new EspecieDocumento_Sofisa(codigoEspecie);
                        break;
                    //655 - Votorantim
                    case 655:
                        _IEspecieDocumento = new EspecieDocumento_Votorantim(codigoEspecie);
                        break;
                    case 707:
                        this._IEspecieDocumento = new EspecieDocumento_Daycoval(codigoEspecie);
                        break;
                    //748 - Sicredi
                    case 748:
                        _IEspecieDocumento = new EspecieDocumento_Sicredi(codigoEspecie);
                        break;
                    //756 - Sicoob
                    case 756:
                        _IEspecieDocumento = new EspecieDocumento_Sicoob(codigoEspecie);
                        break;
                    default:
                        throw new Exception("Código do banco não implementando: " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
        }

        public String CarregarTodasJson(int codigoBanco)
        {
            EspeciesDocumento especiesDocumento = CarregaTodas(codigoBanco);
            return JsonConvert.SerializeObject(especiesDocumento);
        }

        public static EspeciesDocumento CarregaTodas(int codigoBanco)
        {
            try
            {
                switch (codigoBanco)
                {
                    case 1:
                        return EspecieDocumento_BancoBrasil.CarregaTodas();
                    case 4:
                        return EspecieDocumento_Nordeste.CarregaTodas();
                    case 33:
                        return EspecieDocumento_Santander.CarregaTodas();
                    case 41:
                        return EspecieDocumento_Banrisul.CarregaTodas();
                    case 85:
                        return EspecieDocumento_Cecred.CarregaTodas();
                    case 104:
                        return EspecieDocumento_Caixa.CarregaTodas();
                    case 237:
                        return EspecieDocumento_Bradesco.CarregaTodas();
                    case 341:
                        return EspecieDocumento_Itau.CarregaTodas();
                    case 347:
                        return EspecieDocumento_Sudameris.CarregaTodas();
                    case 356:
                        return EspecieDocumento_Real.CarregaTodas();
                    case 399:
                        return EspecieDocumento_HSBC.CarregaTodas();
                    case 422:
                        return EspecieDocumento_Safra.CarregaTodas();
                    case 479:
                        return EspecieDocumento_BankBoston.CarregaTodas();
                    case 748:
                        return EspecieDocumento_Sicredi.CarregaTodas();
                    case 756:
                        return EspecieDocumento_Sicoob.CarregaTodas();
                    default:
                        throw new Exception("Espécies do Documento não implementado para o banco : " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        public static string ValidaSigla(IEspecieDocumento especie)
        {
            try
            {
                return especie.Sigla;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ValidaCodigo(IEspecieDocumento especie)
        {
            try
            {
                return especie.Codigo;
            }
            catch
            {
                return "0";
            }
        }

        public IEspecieDocumento DuplicataMercantil()
        {
            throw new NotImplementedException();
        }

        public IEspecieDocumento DuplicataMercantil(IBanco banco)
        {
            if (!especiesDocumentosBancos.ContainsKey(banco.Codigo))
                throw new BoletoNetException("Espécies de documentos não implementados para o banco.");

            return especiesDocumentosBancos[banco.Codigo].DuplicataMercantil();
        }

        # endregion



        private static Dictionary<int, AbstractEspecieDocumento> especiesDocumentosBancos = new Dictionary<int, AbstractEspecieDocumento>() {
            { 1, new EspecieDocumento_BancoBrasil  ()  },
            { 4, new EspecieDocumento_Nordeste     ()  },
            { 33, new EspecieDocumento_Santander   ()  },
            { 41, new EspecieDocumento_Banrisul    ()  },
            { 85, new EspecieDocumento_Cecred      ()  },
            { 104, new EspecieDocumento_Caixa      ()  },
            { 237, new EspecieDocumento_Bradesco   ()  },
            { 356, new EspecieDocumento_Real       ()  },
            { 341, new EspecieDocumento_Itau       ()  },
            { 347, new EspecieDocumento_Sudameris  ()  },
            { 399, new EspecieDocumento_HSBC       ()  },
            { 479, new EspecieDocumento_BankBoston ()  },
            { 637, new EspecieDocumento_Sofisa     ()  },
            { 707, new EspecieDocumento_Daycoval   ()  },
            { 748, new EspecieDocumento_Sicredi    ()  },
            { 756, new EspecieDocumento_Sicoob     ()  },
        };
    }
}
