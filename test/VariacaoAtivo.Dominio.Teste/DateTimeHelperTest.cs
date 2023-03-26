using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.Helprers;
using Xunit;

namespace VariacaoAtivo.Dominio.Teste
{
    public class DateTimeHelperTest
    {
        private const long TIMESTAMP_VALUE = 1596484800;

        [Theory]
        [InlineData("2020-08-03T17:00:00-03:00")]
        [InlineData("2020-08-03T14:00:00-06:00")]
        [InlineData("2020-08-04T01:00:00+05:00")]
        public void ConvertTimestampToDateTime_DadoQueFoiPassadoUmTimestamp_DeveRetornarDateTimeEsperado(string dataString)
        {
            // Arrange

            DateTime dateTime = Convert.ToDateTime(dataString);
            long timestamp = TIMESTAMP_VALUE;

            // Act
            var dateTimeResultado = DateTimeHelper.ConvertTimestampToDateTime(timestamp);

            // Assert
            Assert.Equal(dateTime.ToLocalTime(), dateTimeResultado.ToLocalTime());
        }


        [Theory]
        [InlineData("2020-08-03T17:00:00-03:00")]
        [InlineData("2020-08-03T14:00:00-06:00")]
        [InlineData("2020-08-04T01:00:00+05:00")]
        public void ConvertToTimestamp_DadoQueFoiPassadoUmDateTimeNaoNulo_DeveRetornarOTimestampCorreto(string dataString)
        {
            // Arrange
            DateTime dateTime = Convert.ToDateTime(dataString);

            // Act
            var timestamp = dateTime.ConvertToTimestamp();

            // Assert
            Assert.Equal(TIMESTAMP_VALUE, timestamp);
        }


    }
}
