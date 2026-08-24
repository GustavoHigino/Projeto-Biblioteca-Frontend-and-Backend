using projetobiblioteca.Pagamentos.Model;
using QRCoder;
using System.Text;

namespace projetobiblioteca.Pagamentos.Gerador
{
    public class GeradorPix
    {
       public static string MontarPaylodPix(Recebedor recebedor)
       {
            string payloadFormat = "000201";
            string gui = "0014br.gov.bcb.pix";
            string chave = $"01{recebedor.ChavePix.Length:D2}{recebedor.ChavePix}";
            string merchantAccount = $"26{gui.Length + chave.Length:D2}{gui}{chave}";
            string merchantCategory = "52040000";
            string transactionCurrency = "5303986";
            string transactionAmount = $"54{recebedor.ValorPix.Length:D2}{recebedor.ValorPix}";
            string countryCode = "5802BR";
            string merchantName = $"59{recebedor.NomeRecebedor.Length:D2}{recebedor.NomeRecebedor}";
            string merchantCity = $"60{recebedor.CidadeRecebedor.Length:D2}{recebedor.CidadeRecebedor}";
            string idCampo = $"05{recebedor.TxtId.Length:D2}{recebedor.TxtId}";
            string additionalData = $"62{idCampo.Length:D2}{idCampo}";
            string payloadSemCrc = $"{payloadFormat}{merchantAccount}" +
                $"{merchantCategory}{transactionCurrency}{transactionAmount}" +
                $"{countryCode}{merchantName}{merchantCity}{additionalData}6304";

            string crc = CalcularCrc16(payloadSemCrc);
            return payloadSemCrc + crc;
        }

        private static string CalcularCrc16(string payloadSemCrc)
        {
            ushort crc = 0xFFFF;
            ushort polynomial = 0x1021;
            byte[] bytes=Encoding.UTF8.GetBytes(payloadSemCrc);
            foreach(byte b in bytes)
            {
                for(int i = 0; i < 8; i++)
                {
                    bool bit = ((b >> (7 - i)) & 1) == 1;
                    bool c15 = ((crc >> 15) & 1) == 1;
                    crc <<= 1;
                    if (c15 ^ bit) crc ^= polynomial;
                }
            }
            return (crc & 0xFFFF).ToString("X4");
        }
        public static string GerarImagemQRCode(string conteudo)
        {
            using (QRCodeGenerator qrGenerator= new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator
                    .CreateQrCode(conteudo,
                    QRCodeGenerator.ECCLevel.Q))
                {
                    using (PngByteQRCode qrCode= new PngByteQRCode
                        (qrCodeData))
                    {
                        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic
                            (20);
                        return Convert.ToBase64String( qrCodeAsPngByteArr);
                    }
                }
            }
        }
    }
}
