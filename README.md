![Logo](https://github.com/fingers10/JqueryDataTablesServerSide/blob/master/AspNetCoreWeb/Images/icon.png)

[![NuGet Badge](https://buildstats.info/nuget/jquerydatatables.serverside.aspnetcoreweb)](https://www.nuget.org/packages/JqueryDatatables.ServerSide.AspNetCoreWeb/)
[![Open Source Love svg1](https://badges.frapsoft.com/os/v1/open-source.svg?v=103)](https://github.com/fingers10/open-source-badges/)

[![GitHub license](https://img.shields.io/github/license/fingers10/JqueryDataTablesServerSide.svg)](https://github.com/fingers10/JqueryDataTablesServerSide/blob/master/LICENSE)
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/graphs/commit-activity)
[![Ask Me Anything !](https://img.shields.io/badge/Ask%20me-anything-1abc9c.svg)](https://GitHub.com/fingers10/JqueryDataTablesServerSide)
[![HitCount](http://hits.dwyl.io/fingers10/badges.svg)](http://hits.dwyl.io/fingers10/badges)

[![GitHub forks](https://img.shields.io/github/forks/fingers10/JqueryDataTablesServerSide.svg?style=social&label=Fork)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/network/)
[![GitHub stars](https://img.shields.io/github/stars/fingers10/JqueryDataTablesServerSide.svg?style=social&label=Star)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/stargazers/)
[![GitHub followers](https://img.shields.io/github/followers/fingers10.svg?style=social&label=Follow)](https://github.com/fingers10?tab=followers)

[![GitHub contributors](https://img.shields.io/github/contributors/fingers10/JqueryDataTablesServerSide.svg)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/graphs/contributors/)
[![GitHub issues](https://img.shields.io/github/issues/fingers10/JqueryDataTablesServerSide.svg)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/issues/)
[![GitHub issues-closed](https://img.shields.io/github/issues-closed/fingers10/JqueryDataTablesServerSide.svg)](https://GitHub.com/fingers10/JqueryDataTablesServerSide/issues?q=is%3Aissue+is%3Aclosed)

# Jquery DataTables Asp.Net Core Server Side
This repository is a Server Side processor for Jquery DataTables with Asp.Net Core as backend. It provides a quick way to implement dynamic multiple column searching and sorting along with pagination and excel export at the server side. This can be done by decorating your model properties with simple attributes.

![Demo](https://github.com/fingers10/JqueryDataTablesServerSideDemo/blob/master/AspNetCoreServerSide/wwwroot/images/demo.gif)

**[Demo Implementation Project URL - Free Download](https://github.com/fingers10/JqueryDataTablesServerSideDemo)**

>**Note: This tutorial contains example for both AJAX GET and AJAX POST Server Side Configuration.**

>**Warning: If we are RESTful strict, we should use GET Method to get information not POST but I prefer this way to avoid limitations related to form data through the query string, so up to you if you want to use GET. I recommend using AJAX GET only if your DataTable has very less number of columns. As Jquery DataTables AJAX requests produces too large query string which will be rejected by server.**

# Wait - Why JqueryDataTablesServerSide ?
Well... there are lots of different approaches how to get a Jquery DataTables with Asp.Net Core app running. I thought it would be nice for .NET devs to use the ASP.NET Core backend and just decorate the model properties with a pretty simple attributes called `[Searchable]` and `[Sortable]`. `[DisplayName(“”)]` as the name suggests, can be used to change the column name in excel export or display name in the table HTML. I just combine ASP.NET Core & Jquery DataTables for easy server side processing.

# Give a Star ⭐️
If you liked `JqueryDataTablesServerSide` project or if it helped you, please give a star ⭐️ for this repository. That will not only help strengthen our .NET community but also improve development skills for .NET developers in around the world. Thank you very much 👍

## Search
* `[Searchable]`
* `[SearchableString]`
* `[SearchableDateTime]`
* `[SearchableShort]`
* `[SearchableInt]`
* `[SearchableLong]`
* `[SearchableDecimal]`
* `[SearchableDouble]`
* `[SearchableEnum(typeof(TEnum))]`
* `[NestedSearchable]`

## Sort
* `[Sortable]`
* `[Sortable(Default = true)]`
* `[NestedSortable]`

## Columns 
### Name
Column names in HTML Table/Excel Export can be configured using the below attributes
* `[Display(Name = "")]`
* `[DisplayName(“”)]`

### Exclude
To exclude any property of your model from being displayed in `<jquery-datatables>` Tag Helper
* `[ExcludeFromJqueryDataTable]`

# Compatibility Chart
>The following chart describes the operator compatibility with data types with green as compatible and red as not compatible.

|Operator|Description|`string`|`DateTime`|`short`|`int`|`long`|`decimal`|`double`|`enum`|
|--------|--------|--------|--------|--------|--------|--------|--------|--------|--------|
|`co`|Contains|:heavy_check_mark:|:x:|:x:|:x:|:x:|:x:|:x:|:x:|
|`eq`|Equals|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|
|`gt`|GreaterThan|:x:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:x:|
|`gte`|GreaterThanEqual|:x:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:x:|
|`lt`|LesserThan|:x:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:x:|
|`lte`|LesserThanEqual|:x:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:x:|

# NuGet:
* [JqueryDataTables.ServerSide.AspNetCoreWeb](https://www.nuget.org/packages/JqueryDataTables.ServerSide.AspNetCoreWeb/) **v3.1.0**

# Usage:
To activate and make Jquery DataTable communicate with asp.net core backend,

## Package Manager:
```c#
PM> Install-Package JqueryDataTables.ServerSide.AspNetCoreWeb
```

## .NET CLI:
```c#
> dotnet add package JqueryDataTables.ServerSide.AspNetCoreWeb
```

# Startup.cs

## Asp.Net Core 3.x:

**Json.NET** has been removed from the ASP.NET Core shared framework.

The default for ASP.NET Core is now `System.Text.Json`, which is new in .NET Core 3.0. Consider using `System.Text.Json` when possible. It's high-performance and doesn't require an additional library dependency. I prefer to use Miscrosoft's new `System.Text.Json`.

With **System.Text.Json**, setup your `ConfigureServices` as follows:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
    services.AddSession();
    services.AddAutoMapper(typeof(Startup));
}
```

If your using **Json.Net**, then add a package reference to ` Microsoft.AspNetCore.Mvc.NewtonsoftJson` and then setup your `ConfigureServices` as follows:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews()
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
    services.AddSession();
    services.AddAutoMapper(typeof(Startup));
}
```

## Asp.Net Core 2.x:

If you're using Asp.Net Core 2.x, then setup your `ConfigureServices` as follows,

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
    services.AddSession();
    services.AddAutoMapper(typeof(Startup));
}
```
    
**Please note:** `services.AddSession()` is is required only if you're using excel export functionality in Jquery DataTables.
    
# Tag Helpers
Add a **JqueryDataTables TagHelper** reference to your `_ViewImports.cshtml` file as shown below

```c#
@addTagHelper *, JqueryDataTables.ServerSide.AspNetCoreWeb
```

# Table HTML
>I have written `<jquery-datatables>` TagHelper to do the heavy lifting works for you.

```c#
<jquery-datatables id="fingers10"
                   class="table table-sm table-dark table-bordered table-hover"
                   model="@Model"
                   thead-class="text-center"
                   enable-searching="true"
                   search-row-th-class="p-0"
                   search-input-class="form-control form-control-sm"
                   search-input-style="width: 100%"
                   search-input-placeholder-prefix="Search">
</jquery-datatables>
```

## TagHelpers Attributes
| Option                            | Description |
|-----------------------------------|-------------|
| `id`                              | to add id to the html table |
| `class`                           | to apply the given css class to the html table |
| `model`                           | view model with properties to generate columns for html table |
| `thead-class`                     | to apply the given css class to the `<thead>` in html table |
| `enable-searching`                | `true` to add search inputs to the `<thead>` and `false` to remove search inputs from the `<thead>` |
| `search-row-th-class`             | to apply the given css class to the search inputs row of the `<thead>` in the html table |
| `search-input-class`              | to apply the given css class to the search input controls added in each column inside `<thead>` |
| `search-input-style`              | to apply the given css styles to the search input controls added in each column inside `<thead>` |
| `search-input-placeholder-prefix` | to apply your placeholder text as prefix in search input controls in each column inside `<thead>` |
    
# Initialize DataTable
>Add the following code to initialize DataTable. Don't miss to add `orderCellsTop : true`. This makes sure to add sorting functionality to the first row in the table header. For other properties refer Jquery DataTables official documentation.

>Use `AdditionalValues` to pass extra parameters if required to the server for processing. Configure Column properties and add the required search operator in the `name` property to perform search based on the operator in the `name` property. If name property is `null` or `string.Empty`, the search default's to `Equals` search operation.  

**Please note:** Search Operator must be one among the following `eq | co | gt | gte | lt | lte` based on the above compatibility chart.

## AJAX POST Configuration

```js
var table = $('#fingers10').DataTable({
        language: {
            processing: "Loading Data...",
            zeroRecords: "No matching records found"
        },
        processing: true,
        serverSide: true,
        orderCellsTop: true,
        autoWidth: true,
        deferRender: true,
        lengthMenu: [5, 10, 15, 20],
        dom: '<"row"<"col-sm-12 col-md-6"B><"col-sm-12 col-md-6 text-right"l>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                text: 'Export to Excel',
                className: 'btn btn-sm btn-dark',
                action: function (e, dt, node, config) {
                    window.location.href = "/Home/GetExcel";
                },
                init: function (api, node, config) {
                    $(node).removeClass('dt-button');
                }
            }
        ],
        ajax: {
            type: "POST",
            url: '/Home/LoadTable/',
            contentType: "application/json; charset=utf-8",
            async: true,
            headers: {
                "XSRF-TOKEN": document.querySelector('[name="__RequestVerificationToken"]').value
            },
            data: function (data) {
                let additionalValues = [];
                additionalValues[0] = "Additional Parameters 1";
                additionalValues[1] = "Additional Parameters 2";
                data.AdditionalValues = additionalValues;

                return JSON.stringify(data);
            }
        },
        columns: [
            {
                data: "Id",
                name: "eq",
                visible: false,
                searchable: false
            },
            {
                data: "Name",
                name: "eq"
            },
            {
                data: "Position",
                name: "co"
            },
            {
                data: "Offices",
                name: "eq"
            },
            {
                data: "DemoNestedLevelOne.Experience",
                name: "eq"
            },
            {
                data: "DemoNestedLevelOne.Extension",
                name: "eq"
            },
            {
                data: "DemoNestedLevelOne.DemoNestedLevelTwos.StartDates",
                render: function (data, type, row) {
                    if (data)
                        return window.moment(data).format("DD/MM/YYYY");
                    else
                        return null;
                },
                name: "gt"
            },
            {
                data: "DemoNestedLevelOne.DemoNestedLevelTwos.Salary",
                name: "lte"
            }
        ]
    });
```

## AJAX GET Configuration

For AJAX GET configuration, simply change the `ajax` and `buttons` options as follows,

```js
buttons: [
            {
                text: 'Export to Excel',
                className: 'btn btn-sm btn-dark',
                action: function (e, dt, node, config) {
                    var data = table.ajax.params();
                    var x = JSON.stringify(data, null, 4);
                    window.location.href = "/Home/GetExcel?" + $.param(data);
                },
                init: function (api, node, config) {
                    $(node).removeClass('dt-button');
                }
            }
        ],
ajax: {
            url: '/Home/LoadTable/',
            data: function (data) {
                return $.extend({}, data, {
                    "additionalValues[0]": "Additional Parameters 1",
                    "additionalValues[1]": "Additional Parameters 2"
                });
            }
       }
```

 # Trigger Search 
>Add the following script to trigger search only onpress of **Enter Key**.

```js
table.columns().every(function (index) {
        $('#fingers10 thead tr:last th:eq(' + index + ') input')
            .on('keyup',
                function (e) {
                    if (e.keyCode === 13) {
                        table.column($(this).parent().index() + ':visible').search(this.value).draw();
                    }
                });
    });
```

>Add the following script to trigger search onpress of **Tab Key**

```js
$('#fingers10 thead tr:last th:eq(' + index + ') input')
    .on('blur',
	    function () {
		    table.column($(this).parent().index() + ':visible').search(this.value).draw();
       });
```

# Model to be passed to DataTable
>Decorate the properties based on their data types. For Nested Complex Properties, decorate them with `[NestedSearchable]`/`[NestedSortable]` attributes upto any level.

## Root Model:

```c#
public class Demo
{
    public int Id { get; set; }

    [SearchableString(EntityProperty = "FirstName,LastName")]
    [Sortable(EntityProperty = "FirstName,LastName", Default = true)]
    public string Name { get => $"{FirstName} {LastName}"; }
    
    [ExcludeFromJqueryDataTable]
    public string FirstName { get; set; }

    [ExcludeFromJqueryDataTable]
    public string LastName { get; set; }

    [SearchableEnum(typeof(Position))]
    [Sortable]
    public string Position { get; set; }

    [Display(Name = "Office")]
    [SearchableString(EntityProperty = "Office")]
    [Sortable(EntityProperty = "Office")]
    public string Offices { get; set; }

    [NestedSearchable]
    [NestedSortable]
    public DemoNestedLevelOne DemoNestedLevelOne { get; set; }
}
```

## Nested Level One Model:

```c#
public class DemoNestedLevelOne
{
    [SearchableShort]
    [Sortable]
    public short? Experience { get; set; }

    [DisplayName("Extn")]
    [SearchableInt(EntityProperty = "Extn")]
    [Sortable(EntityProperty = "Extn")]
    public int? Extension { get; set; }

    [NestedSearchable(ParentEntityProperty = "DemoNestedLevelTwo")]
    [NestedSortable(ParentEntityProperty = "DemoNestedLevelTwo")]
    public DemoNestedLevelTwo DemoNestedLevelTwos { get; set; }
}
```

## Nested Level Two Model:

```c#
public class DemoNestedLevelTwo
{
    [SearchableDateTime(EntityProperty = "StartDate")]
    [Sortable(EntityProperty = "StartDate")]
    [DisplayName("Start Date")]
    public DateTime? StartDates { get; set; }

    [SearchableLong]
    [Sortable]
    public long? Salary { get; set; }
}
```

**Please note:** 
* If view model properties have different name than entity model, then you can still do mapping using `(EntityProperty = 'YourEntityPropertyName')`. If they are same then you can ignore this.
* If view model property is a combination of some other properties like `Name` property in the above root model, then you can specify them in `(EntityProperty = 'YourEntityPropertyName,YourSomeOtherEntityPropertyName')`. This will make an implicit `OR` search in database and sort by `YourEntityPropertyName` and then by `YourSomeOtherEntityPropertyName` in database. For Example, take the `Name` property in root model. It has `[SearchableString(EntityProperty = "FirstName,LastName")]`. This will generate a implicit `OR` query like `entity.Where(x => x.FirstName.ToLower().Contains("Name") || x.LastName.ToLower().Contains("Name"))`. Similarly, `[Sortable(EntityProperty = "FirstName,LastName", Default = true)]` will generate query like `entity.OrderBy(x => x.FirstName).ThenBy(x => x.LastName)` for ascending order and `entity.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName)` for descending order.
    
# ActionMethod/PageHandler
>On DataTable's AJAX Request, `JqueryDataTablesParameters` will read the DataTable's state and `JqueryDataTablesResult<T>` will accept `IEnumerable<T>` response data to be returned back to table as `JsonResult`.

## AJAX POST Configuration

### ActionMethod

```c#
[HttpPost]
public async Task<IActionResult> LoadTable([FromBody]JqueryDataTablesParameters param)
{
    try
    {
        // `param` is stored in session to be used for excel export. This is required only for AJAX POST.
        // Below session storage line can be removed if you're not using excel export functionality. 
	// If you're using Json.Net, then uncomment below line else remove below line
	// HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonConvert.SerializeObject(param));
	// If you're using new System.Text.Json then use below line
        HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
        var results = await _demoService.GetDataAsync(param);

        return new JsonResult(new JqueryDataTablesResult<Demo> {
            Draw = param.Draw,
            Data = results.Items,
            RecordsFiltered = results.TotalSize,
            RecordsTotal = results.TotalSize
        });
    } catch(Exception e)
    {
        Console.Write(e.Message);
        return new JsonResult(new { error = "Internal Server Error" });
    }
}
```

### PageHandler

```c#
public async Task<IActionResult> OnPostLoadTableAsync([FromBody]JqueryDataTablesParameters param)
{
    try
    {
        // `param` is stored in session to be used for excel export. This is required only for AJAX POST.
        // Below session storage line can be removed if you're not using excel export functionality. 
	// If you're using Json.Net, then uncomment below line else remove below line
	// HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonConvert.SerializeObject(param));
	// If you're using new System.Text.Json then use below line
        HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
        var results = await _demoService.GetDataAsync(param);

        return new JsonResult(new JqueryDataTablesResult<Demo> {
            Draw = param.Draw,
            Data = results.Items,
            RecordsFiltered = results.TotalSize,
            RecordsTotal = results.TotalSize
        });
    } catch(Exception e)
    {
        Console.Write(e.Message);
        return new JsonResult(new { error = "Internal Server Error" });
    }
}
```

## AJAX GET Configuration

### ActionMethod

```c#
public async Task<IActionResult> LoadTable([ModelBinder(typeof(JqueryDataTablesBinder))] JqueryDataTablesParameters param)
{
    try
    {
        var results = await _demoService.GetDataAsync(param);

        return new JsonResult(new JqueryDataTablesResult<Demo> {
            Draw = param.Draw,
            Data = results.Items,
            RecordsFiltered = results.TotalSize,
            RecordsTotal = results.TotalSize
        });
    } catch(Exception e)
    {
        Console.Write(e.Message);
        return new JsonResult(new { error = "Internal Server Error" });
    }
}
```

### PageHandler

```c#
public async Task<IActionResult> OnGetLoadTableAsync([ModelBinder(typeof(JqueryDataTablesBinder))] JqueryDataTablesParameters param)
{
    try
    {
        var results = await _demoService.GetDataAsync(param);

        return new JsonResult(new JqueryDataTablesResult<Demo> {
            Draw = param.Draw,
            Data = results.Items,
            RecordsFiltered = results.TotalSize,
            RecordsTotal = results.TotalSize
        });
    } catch(Exception e)
    {
        Console.Write(e.Message);
        return new JsonResult(new { error = "Internal Server Error" });
    }
}
```
# Multiple Column Searching and Sorting
>Inject Automapper `IConfigurationProvider` to make use of `ProjectTo<T>` before returning the data. Inside the Data Access Method, create `IQueryable<TEntity>` to hold the query. Now, to perform dynamic multiple column **searching** use Static Search Processor `SearchOptionsProcessor<T,TEntity>` and call the `Apply()` function with query and table columns as parameters. Again for dynamic multiple column **sorting**, use Static Sort Processor `SortOptionsProcessor<T,TEntity>` and call the `Apply()` function with query and table as parameters. To implement **pagination**, make use of `Start` and `Length` from table parameter and return the result as `JqueryDataTablesPagedResults`.

```c#    
public class DefaultDemoService:IDemoService
{
    private readonly Fingers10DbContext _context;
    private readonly IConfigurationProvider _mappingConfiguration;

    public DefaultDemoService(Fingers10DbContext context,IConfigurationProvider mappingConfiguration)
    {
        _context = context;
        _mappingConfiguration = mappingConfiguration;
    }

    public async Task<JqueryDataTablesPagedResults<Demo>> GetDataAsync(JqueryDataTablesParameters table)
    {
        IQueryable<DemoEntity> query = _context.Demos
                                               .AsNoTracking()
                                               .Include(x => x.DemoNestedLevelOne)
                                               .ThenInclude(y => y.DemoNestedLevelTwo);
        query = SearchOptionsProcessor<Demo,DemoEntity>.Apply(query,table.Columns);
        query = SortOptionsProcessor<Demo,DemoEntity>.Apply(query,table);

        var size = await query.CountAsync();

        var items = await query
            .AsNoTracking()
            .Skip((table.Start / table.Length) * table.Length)
            .Take(table.Length)
            .ProjectTo<Demo>(_mappingConfiguration)
            .ToArrayAsync();

        return new JqueryDataTablesPagedResults<Demo> {
            Items = items,
            TotalSize = size
        };
    }
}
 ```   
 **Please note:** If you are having DataAccessLogic in a separate project, the create instance of `SearchOptionsProcessor` and `SortOptionsProcessor` inside **ActionMethod/Handler** and pass it as a parameter to Data Access Logic.
 
 # Excel Export
 To exporting the filtered and sorted data as an excel file, add `GetExcel` action method in your controller as shown below. Return the   data as `JqueryDataTablesExcelResult<T>` by passing filtered/ordered data, excel sheet name and excel file name. **JqueryDataTablesExcelResult** Action Result that I have added in the Nuget package. This will take care of converting your data as excel file and return it back to browser.
 
 >If you want all the results in excel export without pagination, then please write a **separate service method** to retrive data without using `Take()` and `Skip()`
 
 ## AJAX POST Configuration
 
 ### Action Method
 
 ```c#
 public async Task<IActionResult> GetExcel()
 {
    // Here we will be getting the param that we have stored in the session in server side action method/page handler
    // and deserialize it to get the required data.
    var param = HttpContext.Session.GetString(nameof(JqueryDataTablesParameters));

    // If you're using Json.Net, then uncomment below line else remove below line
    // var results = await _demoService.GetDataAsync(JsonConvert.DeserializeObject<JqueryDataTablesParameters>(param));
    // If you're using new System.Text.Json then use below line
    var results = await _demoService.GetDataAsync(JsonSerializer.Deserialize<JqueryDataTablesParameters>(param));
    return new JqueryDataTablesExcelResult<DemoExcel>(_mapper.Map<List<DemoExcel>>(results.Items),"Demo Sheet Name","Fingers10");
 }
  ```   
  
 ### Page Handler
  
  ```c#
 public async Task<IActionResult> OnGetExcelAsync()
 {
    // Here we will be getting the param that we have stored in the session in server side action method/page handler
    // and deserialize it to get the required data.
    var param = HttpContext.Session.GetString(nameof(JqueryDataTablesParameters));

    // If you're using Json.Net, then uncomment below line else remove below line
    // var results = await _demoService.GetDataAsync(JsonConvert.DeserializeObject<JqueryDataTablesParameters>(param));
    // If you're using new System.Text.Json then use below line
    var results = await _demoService.GetDataAsync(JsonSerializer.Deserialize<JqueryDataTablesParameters>(param));
    return new JqueryDataTablesExcelResult<DemoExcel>(_mapper.Map<List<DemoExcel>>(results.Items),"Demo Sheet Name","Fingers10");
 }
 ```   
 
 ## AJAX GET Configuration
 
 ### Action Method
 
```c#
public async Task<IActionResult> GetExcel([ModelBinder(typeof(JqueryDataTablesBinder))] JqueryDataTablesParameters param)
{
    var results = await _demoService.GetDataAsync(param);
    return new JqueryDataTablesExcelResult<DemoExcel>(_mapper.Map<List<DemoExcel>>(results.Items),"Demo Sheet Name","Fingers10");
}
```
 
 ### Page Handler
 
```c#
public async Task<IActionResult> OnGetExcelAsync([ModelBinder(typeof(JqueryDataTablesBinder))] JqueryDataTablesParameters param)
{
    var results = await _demoService.GetDataAsync(param);
    return new JqueryDataTablesExcelResult<DemoExcel>(_mapper.Map<List<DemoExcel>>(results.Items),"Demo Sheet Name","Fingers10");
}
```
 
 **Please note:** `GetExcel` **ActionMethod/Handler** name must match the name you define in the excel export action click in your Jquery DataTable Initialization script. For getting excel file from complex models/mappings, either project the results to final simple model or use automapper flattening feature to map the results from complex model to simple model.
 
 # Coming Soon
 JqueryDataTablesServerSide is actively under development and I plan to have even more useful features implemented soon, including:
 * Dynamic Select
 * More Helpers
 
 Get in touch if there are any features you feel JqueryDataTablesServerSide needs.
 
 # Target Platform
 * .Net Standard 2.0
 
 # Tools Used
 * Visual Studio Community 2019
 
 # Other Nuget Packages Used
 * Fingers10.ExcelExport (1.0.0) - For Generating Excel Report
 * Microsoft.AspNetCore.Razor (2.2.0) - For using TagHelper
 * Newtonsoft.Json (12.0.2) - For Serialization/Deserialization
 * System.Text.Json (4.6.0) - For Serialization/Deserialization
 
 # Author
 * **Abdul Rahman** - Software Developer - from India. Software Consultant, Architect, Freelance Lecturer/Developer and Web Geek.  
 
 # Contributions
 Feel free to submit a pull request if you can add additional functionality or find any bugs (to see a list of active issues), visit the   Issues section. Please make sure all commits are properly documented.
 
 Many thanks to the below developers for helping with PR's and suggesting Features:
 * [@gaugo123](https://github.com/gaugo123) - gaugo123
 * [@cihangll](https://github.com/cihangll) - Cihan Güllü
 * [@JudeVajira](https://github.com/JudeVajira) - Jude Vajira Guanasekera
  
 # License
 JqueryDataTablesServerSide is release under the MIT license. You are free to use, modify and distribute this software, as long as the copyright header is left intact.

 Enjoy!

 # Sponsors/Backers
 I'm happy to help you with my Nuget Package. Support this project by becoming a sponsor/backer. Your logo will show up here with a link to your website. Sponsor/Back via  [![Sponsor via PayPal](https://www.paypalobjects.com/webstatic/mktg/Logo/pp-logo-100px.png)](https://paypal.me/arsmsi)
