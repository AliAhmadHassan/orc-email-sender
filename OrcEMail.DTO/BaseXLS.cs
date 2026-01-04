using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcEMail.DTO
{
    public class BaseXLS
    {
        [XLSX_Planilha.AtributoXLS(Linha = 1, Coluna = 0)]
        [XLSX_Coluna.AtributoXLS_Coluna(Ordem=0)]
        public string CPF { get; set; }

        [XLSX_Coluna.AtributoXLS_Coluna(Ordem=1)]
        public string EMail1 { get; set; }

        [XLSX_Coluna.AtributoXLS_Coluna(Ordem = 2)]
        public string EMail2 { get; set; }

        [XLSX_Coluna.AtributoXLS_Coluna(Ordem = 3)]
        public string EMail3 { get; set; }

        [XLSX_Coluna.AtributoXLS_Coluna(Ordem = 4)]
        public string EMail4 { get; set; }
    }
}
