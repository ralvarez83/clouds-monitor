using SystemAdministrator.LastBackups.Application.Dtos;
using Xunit.Gherkin.Quick;

namespace WebAPI.GetLast;

[FeatureFile("./GetLast/GetLastBackups.feature")]
public sealed class GetLastBackups : Feature
{
  private HttpResponseMessage? _response { get; set; }

  [Given(@"I send a GET request to '(.*)'")]
  public async Task Given_I_Send_A_Get_Request(string url)
  {
    HttpClient client = new HttpClient
    {
      BaseAddress = new Uri("https://localhost:7259")
    };
    client.DefaultRequestHeaders.Accept.Clear();

    _response = await client.GetAsync(url);
  }

  [Then(@"the response status code should be (\d+)")]
  public void Then_Respons_Should_Be(int statusCode)
  {
    Assert.NotNull(this._response);
    Assert.Equal<int>(statusCode, (int)this._response.StatusCode);
  }

  [And(@"the result should return a not empty list")]
  public async Task Then_Should_Return_Not_Empty_Backup_List()
  {
    Assert.NotNull(this._response);
    MachineDto[] backups = await this._response.Content.ReadAsAsync<MachineDto[]>();
    Assert.NotEmpty(backups);
  }

}