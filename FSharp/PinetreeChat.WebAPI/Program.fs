namespace PinetreeChat

module WebApi = 
    open System
    open Microsoft.AspNetCore.Builder
    open Microsoft.AspNetCore.Cors.Infrastructure
    open Microsoft.AspNetCore.Hosting
    open Microsoft.Extensions.Logging
    open Microsoft.Extensions.DependencyInjection
    open Microsoft.AspNetCore.Http
    open Giraffe
    open PinetreeChat.WebAPI.SignalR

    // WEB APP
    let webApp =
        choose [
            subRoute "/api/chat" PinetreeChat.WebAPI.ChatAPI.routeHandler 
            subRoute "/api/user" PinetreeChat.WebAPI.UserAPI.routeHandler  
            setStatusCode 404 >=> text "Not found"
        ]

    // ERROR HANDLER
    let errorHandler (ex : Exception) (logger : ILogger)=
        logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
        clearResponse >=> setStatusCode 500 >=> text ex.Message

    // CONFIG

    let configureCors (builder : CorsPolicyBuilder) =
        builder.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
            |> ignore

    let configureApp (app : IApplicationBuilder) =
        app.UseCors(configureCors)            
           .UsePathBase(new PathString("/PinetreeChat.FSharpAPI"))
           .UseSignalR(fun(r) -> r.MapHub<ChatHub>("chatHub"))
           .UseGiraffeErrorHandler(errorHandler)
           .UseGiraffe(webApp)

    let configureServices (services: IServiceCollection) =
        services.AddCors()
                .AddSignalR() |> ignore


    [<EntryPoint>]
    let main argv =
        WebHostBuilder().UseKestrel()
                        .UseUrls("http://localhost:777/")
                        .UseIISIntegration()
                        .Configure(Action<IApplicationBuilder> configureApp)
                        .ConfigureServices(configureServices)
                        .Build()
                        .Run()
        0 // return an integer exit code
