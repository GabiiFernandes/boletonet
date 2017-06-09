using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BoletoNet
{

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

        #region Instrução

        public Instrucao newInstrucao(int codigoBanco)
        {
            return new Instrucao(codigoBanco);
        }

        #endregion

        #region Banco

        public Banco newBanco(int codigoBanco)
        {
            return new Banco(codigoBanco);
        }

        #endregion
    }
}
