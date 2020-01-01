In an ever growing software world, many times we as a developer feel it would be better if we can monitor the performance cost of our development live. It is really helpful to figure out the performance issue early as it can save both time and cost to the organizations. MiniProfiler helps us in solving the performance problem and gives developer more control over testing and finding the root cause after deploying code in Test and Production environment.

As per mini-profiler website:

- MiniProfiler is a library and UI for profiling your application. By letting you see where your time is spent, which queries are run, and any other custom timings you want to add, MiniProfiler helps you debug issues and optimize performance.
- The default setup is for every page to have an indicator so performance is always on your mind, like this:
  - ![image](https://miniprofiler.com/dotnet/images/Popup.png)

Any custom timings (like queries) can be expanded in detail as well:

![image](https://miniprofiler.com/dotnet/images/Timings.png)

Now lets see the above in action. We will use a sample app build using Asp .Net core MVC along with SQL server local DB.

1. About our app:
   1. Our app contains a web api call on page load
   2. On submit we will insert data into database(I am using a SQL local DB. I have shared the script in git repo)
   3. Once the project is set lets start with miniprofiler configuration

2. Install nuget **miniprofiler.aspnetcore.mvc** on your web project

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/v8fyi6fzbdxf9ac5xcux.png)

3. Add below highlighted lines in ConfigureServices():

   There are many other configuration which can be used as per project requirement, but i am going with minimal configuration.

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/ll8i4u165ai0n1ho46ka.png)

4. Add below highlighted lines in Configure()

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/9tlgjwaaikmme5w754ao.png)

5. Modify _ViewImport view with below code:

 ![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/ns2140a0i3o4c7n7plzv.png)

6. Now we are good to test our web app. As you can see below our popup is displayed on bottom left as we have configured in ConfigureServices()

   On load we are making an api call and we can see the details of how much time the API is taking.

   **Note:** Please use a pipeline to better control the HTTP calls as it is not possible to configure miniprofiler for each call.

   ![](C:\Users\singh4m\OneDrive - kochind.com\Documents\Personal\Learn\PS\Core n Sql\miniprofiler\Blog\OnLoadAPICall.png)

   Also when we click on the Try button on home page we are making a DB call

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/coo1cb48bo8x0wjzfcfc.png)

Incase we need more details of our SQL calls, add miniprofiler.entityframeworkcore nuget package to your Web project:

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/p5o40l1dogomg16fzahe.png)

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/io3djchm49yvw9plqpyf.png)

Now if we reload the application we can see in detail what DB calls are happening by clicking on sql time taken.

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/s6gr04duyk7qaf8qkoh0.png)

![Alt Text](https://thepracticaldev.s3.amazonaws.com/i/3byyjr9tutf12laahi0l.png)

Incase if you are using ADO .Net and not using EnityFramework, it is easy to modify the data access layer and use ProfiledDbConnection to return the DbConnection object. Please refer below code snippet.

```c#
public DbConnection GetConnection()
{
    DbConnection connection = new System.Data.SqlClient.SqlConnection("...");
    return new StackExchange.Profiling.Data.ProfiledDbConnection(connection, MiniProfiler.Current);
}
```

Generally we tend to have a large functionality which is broken into multiple methods, we can still use miniprofiler to figure out which particular method is causing trouble.

Nuget required: **miniprofiler.aspnetcore**

```c#
using (StackExchange.Profiling.MiniProfiler.Current.Step("Method1 begins"))
{
    //Method1()
}

using (StackExchange.Profiling.MiniProfiler.Current.Step("Method2 begins"))
{
    //Method2()
}

using (StackExchange.Profiling.MiniProfiler.Current.Step("Method3 begins"))
{
    //Method3()
}
```

#### How to secure the information?

Now as we know,

![](https://i.pinimg.com/originals/ae/20/73/ae207372837cec0d56db1db481893176.jpg)

We need to display these information only to our DEV teams. Now we can control the access using Roles information present in HttpContext as shown below.

```c#
public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<UtilityToolContext>();
            services.AddMiniProfiler(profiler => {
                profiler.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                profiler.PopupShowTimeWithChildren = true;
                profiler.ResultsAuthorize = (request) => request.HttpContext.User.IsInRole("DEV");
            }).AddEntityFramework();
        }
```

#### Is it fast to be used in large applications?

![](https://media1.giphy.com/media/3o6Ygosf6ZRBYyEdMc/giphy.gif)

Incase if you are worried about its performance in production, please note that **MiniProfiler was designed by the team at <u>Stack Overflow</u>. It is in production use there and on the [Stack Exchange](https://stackexchange.com/) family of sites.**

References:

https://miniprofiler.com/

https://miniprofiler.com/dotnet/

https://miniprofiler.com/dotnet/HowTo/ProfileSql