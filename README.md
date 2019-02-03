![Logo](https://github.com/fingers10/JqueryDataTablesServerSide/blob/master/AspNetCoreWeb/Icons/logo.png)

# Jquery DataTables Asp.Net Core Server Side
This repository is a Server Side processor for Jquery DataTables with Asp.Net Core as backend. It provides a quick way to implement dynamic multiple column searching and sorting at the server side. This can be done by decorating your model properties with simple attributes.

# Wait - Why JqueryDataTablesServerSide ?
Well... there are lots of different approaches how to get a Jquery DataTables with Asp.Net Core app running. I thought it would be nice for .NET devs to use the ASP.NET Core backend and just decorate the model properties with a pretty simple attributes called `[Searchable]` and `[Sortable]`. I just combine ASP.NET Core & Jquery DataTables for easy server side processing.

## Search
* `[Searchable]`
* `[SearchableString]`
* `[SearchableDateTime]`
* `[SearchableShort]`
* `[SearchableInt]`
* `[SearchableLong]`
* `[SearchableDecimal]`
* `[SearchableDouble]`

## Sort
* `[Sortable]`
* `[Sortable(Default = true)]`

# Compatibility Chart
>The following chart describes the operator compatibility with data types with green as compatible and red as not compatible.

![Compatibility Chart](https://github.com/fingers10/JqueryDataTablesServerSide/blob/master/AspNetCoreWeb/Icons/compatibility-chart.PNG)

# NuGet:
* [JqueryDataTables.ServerSide.AspNetCoreWeb](https://www.nuget.org/packages/JqueryDataTables.ServerSide.AspNetCoreWeb/) **v1.2.1**

# Usage:
To activate and make Jquery DataTable communicate with asp.net core backend,

    PM> Install-Package JqueryDataTables.ServerSide.AspNetCoreWeb

# Startup.cs
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN")
            .AddMvc()
            // This must be set for Jquery DataTables to work
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
         
            // Add AutoMapper to Map Entity to Model
        services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
    }

**Please note:** Json Serializer Setting's ContractResolver must be set to `new DefaultContractResolver()`

# Table HTML
>Add an empty second `<tr>` in `<thead>` to add search `inputs`.

    <table id="fingers10" class="table table-sm table-dark table-bordered table-hover">
      <thead class="text-center">
        <tr>
          <th>Name</th>
          <th>Position</th>
          <th>Office</th>
          <th>Extn</th>
          <th>Start Date</th>
          <th>Salary</th>
        </tr>
        <tr>
          <th></th>
          <th></th>
          <th></th>
          <th></th>
          <th></th>
          <th></th>
          </tr>
      </thead>
    </table>
    
# Adding Search Inputs
>Add the following script to append search `input` dynamically.

    $('#fingers10 thead tr:last th').each(function () {
      var label = $('#fingers10 thead tr:first th:eq(' + $(this).index() + ')').html();
      $(this)
          .addClass('p-0')
          .html('<span class="sr-only">' + label + '</span><input type="search" class="form-control form-control-sm" aria-label="' + label + '" />');
    });
    
# Initialize DataTable
>Add the following code to initialize DataTable. Don't miss to add `orderCellsTop : true`. This makes sure to add sorting functionality to the first row in the table header. For other properties refer Jquery DataTables official documentation.

>Use `AdditionalValues` to pass extra parameters if required to the server for processing. Configure Column properties and add the required search operator in the `name` property to perform search based on the operator in the `name` property. If name property is `null` or `string.Empty`, the search default's to `Equals` search operation.  

**Please note:** Search Operator must be one among the following `eq | co | gt | gte | lt | lte` based on the above compatibility chart.

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
            dom: '<tr>',
            ajax: {
                type: "POST",
                url: '/Home/LoadTable/',
                contentType: "application/json; charset=utf-8",
                async: true,
                headers: {
                    "XSRF-TOKEN" : document.querySelector('[name="__RequestVerificationToken"]').value
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
                    title: "Name",
                    data: "Name",
                    name: "eq"
                },
                {
                    title: "Position",
                    data: "Position",
                    name: "co"
                },
                {
                    title: "Office",
                    data: "Office",
                    name: "eq"
                },
                {
                    title: "Extn",
                    data: "Extn",
                    name: "eq"
                },
                {
                    title: "Start Date",
                    data: "StartDate",
                    render: function (data, type, row) {
                        return window.moment(row.StartDate).format("DD/MM/YYYY");
                    },
                    name: "gt"
                },
                {
                    title: "Salary",
                    data: "Salary",
                    name: "lte"
                }
            ]
        });

 # Trigger Search 
>Add the following script to trigger search only onpress of **Enter Key**.

    table.columns().every(function (index) {
            $('#fingers10 thead tr:last th:eq(' + index + ') input')
                .on('keyup',
                    function (e) {
                        if (e.keyCode === 13) {
                            table.column($(this).parent().index() + ':visible').search(this.value).draw();
                        }
                    });
        });

# Model to be passed to DataTable
>Decorate the properties based on their data types

    public class Demo
    {
        public int Id { get; set; }

        [SearchableString]
        [Sortable(Default = true)]
        public string Name { get; set; }

        [SearchableString]
        [Sortable]
        public string Position { get; set; }

        [SearchableString]
        [Sortable]
        public string Office { get; set; }

        [SearchableInt]
        [Sortable]
        public int Extn { get; set; }

        [SearchableDateTime]
        [Sortable]
        public DateTime StartDate { get; set; }

        [SearchableLong]
        [Sortable]
        public long Salary { get; set; }
    }
    
# ActionMethod/PageHandler
>On DataTable's Ajax Post Request, `DTParameters` will read the DataTable's state and `DTResult<T>` will accept `IEnumerable<T>` response data to be returned back to table as `JsonResult`.

    [HttpPost]
    public async Task<IActionResult> LoadTable([FromBody]DTParameters param)
    {
        try
        {
            var data = await _demoService.GetDataAsync(param);

            return new JsonResult(new DTResult<Demo> {
                draw = param.Draw,
                data = data,
                recordsFiltered = data.Length,
                recordsTotal = data.Length
            });
        } catch(Exception e)
        {
            Console.Write(e.Message);
            return new JsonResult(new { error = "Internal Server Error" });
        }
    }

# Multiple Column Searching and Sorting
>Inject Automapper `IConfigurationProvider` to make use of `ProjectTo<T>` before returning the data. Inside the Data Access Method, create `IQueryable<TEntity>` to hold the query. Now, to perform dynamic multiple column **searching** create a instance of Search Processor `new SearchOptionsProcessor<T,TEntity>()` and call the `Apply()` function with query and table columns as parameters. Again for dynamic multiple column **sorting**, create a instance of Sort Processor `new SortOptionsProcessor<T,TEntity>()` and call the `Apply()` function with query and table as parameters. To implement pagination, make use of `Start` and `Length` from table parameter.
    
    public class DefaultDemoService:IDemoService
    {
        private readonly Fingers10DbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultDemoService(Fingers10DbContext context,IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<Demo[]> GetDataAsync(DTParameters table)
        {
            IQueryable<DemoEntity> query = _context.Demos;
            query = new SearchOptionsProcessor<Demo,DemoEntity>().Apply(query,table.Columns);
            query = new SortOptionsProcessor<Demo,DemoEntity>().Apply(query,table);

            var items = await query
                .AsNoTracking()
                .Skip(table.Start - 1 * table.Length)
                .Take(table.Length)
                .ProjectTo<Demo>(_mappingConfiguration)
                .ToArrayAsync();

            return items;
        }
    }
    
 **Please note:** If you are having DataAccessLogic in a separate project, the create instance of `SearchOptionsProcessor` and `SortOptionsProcessor` inside **ActionMethod/Handler** and pass it as a parameter to Data Access Logic.
 
 # Author
 * **Abdul Rahman** - Software Developer - from India. Software Consultant, Architect, Freelance Lecture/Developer and Web Geek.  
 
 # Contributions
 Feel free to submit a pull request if you can add additional functionality or find any bugs (to see a list of active issues, visit the   Issues section. Please make sure all commits are properly documented.
  
 # License
 MIT-licensed

 Enjoy!
