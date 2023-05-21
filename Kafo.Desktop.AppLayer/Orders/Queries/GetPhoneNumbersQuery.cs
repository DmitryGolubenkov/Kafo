using Kafo.Application.Models.Orders;
using Kafo.Desktop.AppLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafo.Desktop.AppLayer.Orders.Queries;
public class GetPhoneNumbersQuery
{
    #region Fields

    private readonly AuthenticatedHttpClient httpClient;

    #endregion

    #region Constructor

    public GetPhoneNumbersQuery(AuthenticatedHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    #endregion

    #region Methods

    public async Task<List<string>> Execute()
    {
        return await httpClient.GetAuthenticatedAsync<List<string>>("api/users/getPhoneNumbers");
    }

    #endregion

}
