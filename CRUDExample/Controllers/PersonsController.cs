using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
  [Route("[controller]")]
  public class PersonsController : Controller
  {
    //private fields
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;

    //constructor
    public PersonsController(IPersonsService personsService, ICountriesService countriesService)
    {
      _personsService = personsService;
      _countriesService = countriesService;
    }

    //the url is persons/index
    [Route("[action]")]
    [Route("/")]
    public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
    {
      //Search
      ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PersonResponse.PersonName), "Person Name" },
        { nameof(PersonResponse.Email), "Email" },
        { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryID), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };
      List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);
      ViewBag.CurrentSearchBy = searchBy;
      ViewBag.CurrentSearchString = searchString;

      //Sort
      List<PersonResponse> sortedPersons =  _personsService.GetSortedPersons(persons, sortBy, sortOrder);
      ViewBag.CurrentSortBy = sortBy;
      ViewBag.CurrentSortOrder = sortOrder.ToString();

      return View(sortedPersons); //Views/Persons/Index.cshtml
    }


    //this method executes when the user clicks on "Create Person" hyperlink.
    //the url is persons/create
    [Route("[action]")]
    [HttpGet]
    public IActionResult Create()
    {
      List<CountryResponse> countries = _countriesService.GetAllCountries();
      ViewBag.Countries = countries.Select(temp => 
        new SelectListItem() {  Text = temp.CountryName, Value = temp.CountryID.ToString() }
      );

      return View();
    }

    [HttpPost]
    //the url is persons/create
    [Route("[action]")]
    public IActionResult Create(PersonAddRequest personAddRequest)
    {
      if (!ModelState.IsValid)
      {
        List<CountryResponse> countries = _countriesService.GetAllCountries();
        ViewBag.Countries = countries.Select(temp =>
        new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

        ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return View();
      }

      //here we are calling the service method
      PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
      
      //here we are navigateing to Index() action method, so it makes another get request to "persons/index"
      return RedirectToAction("Index", "Persons");
    }

    [HttpGet]
    [Route("[action]/{personID}")]
    public IActionResult Edit(Guid personID)
    {
      PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);
      if (personResponse == null)
      {
        return RedirectToAction("Index");
      }

      PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

      List<CountryResponse> countries = _countriesService.GetAllCountries();
      ViewBag.Countries = countries.Select(temp =>
      new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

      return View(personUpdateRequest);
    }


    [HttpPost]
    [Route("[action]/{personID}")]
    public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
    {
      PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);

      if (personResponse == null)
      {
        return RedirectToAction("Index");
      }

      if (ModelState.IsValid)
      {
        PersonResponse updatedPerson = _personsService.UpdatePerson(personUpdateRequest);
        return RedirectToAction("Index");
      }
      else
      {
        List<CountryResponse> countries = _countriesService.GetAllCountries();
        ViewBag.Countries = countries.Select(temp =>
        new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

        ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return View(personResponse.ToPersonUpdateRequest());
      }
    }


    [HttpGet]
    [Route("[action]/{personID}")]
    public IActionResult Delete(Guid? personID)
    {
      PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);
      if (personResponse == null)
        return RedirectToAction("Index");

      return View(personResponse);
    }

    [HttpPost]
    [Route("[action]/{personID}")]
    public IActionResult Delete(PersonUpdateRequest personUpdateResult)
    {
      PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateResult.PersonID);
      if (personResponse == null)
        return RedirectToAction("Index");

      _personsService.DeletePerson(personUpdateResult.PersonID);
      return RedirectToAction("Index");
    }
  }
}
