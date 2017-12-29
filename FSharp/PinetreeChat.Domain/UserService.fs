namespace PinetreeChat.Domain

module UserService = 
    open PinetreeChat.Entities
    open PinetreeChat.Domain.Validation
    open Chessie.ErrorHandling

    type UserErrorMessage =
        | UserLoggedIn of string 
        | UsernameInvalid of string    
        
    let getErrorMessage e =
        match e with
        | UserLoggedIn msg -> msg
        | UsernameInvalid msg -> msg

    let login getUserFunction addUserFunction setLoggedInFunction username =
        let notEmpty = validateEmptyString (UsernameInvalid "Username is empty")
        let lengthValid = validateStringLength 1 100 (UsernameInvalid "Username has wrong length") 

        let validationResult un = notEmpty un <* lengthValid un  
        
        let loginUser un =
            match getUserFunction un with
            | Some user -> 
                match user.IsLoggedIn with
                | true -> Bad [ (UserLoggedIn "User is already logged in") ]
                | false -> 
                    setLoggedInFunction username |> ok
            | None ->
                { Id = 0; Username = un; IsLoggedIn = true } 
                |> addUserFunction
                |> ok

        username |> validationResult >>= loginUser

    let logout getUserFunction setLoggedOutFunction username =
        match getUserFunction username with 
        | Some _ -> 
            setLoggedOutFunction username
            |> ok 
        | None -> Bad [ (UsernameInvalid "This user does not exist") ]


module UserServiceWithDataAccess =
    open PinetreeChat.DataAccess.UserRepository    
    let login = UserService.login getUser addUser setLoggedIn
    let logout = UserService.logout getUser setLoggedOut

            
        