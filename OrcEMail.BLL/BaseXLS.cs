using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcEMail.BLL
{
    public class BaseXLS
    {
        public List<DTO.BaseXLS> ImportaBase(string CaminhoArquivo)
        {
            List<DTO.BaseXLS> baseXLS = new DAL.XLSX().RetornaEntidade<DTO.BaseXLS>(CaminhoArquivo);

            return baseXLS;
        }
    }
}
