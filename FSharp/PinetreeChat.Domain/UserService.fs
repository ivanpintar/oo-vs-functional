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

    let login findUserFunction addUserFunction setLoggedInFunction username =
        let notEmpty = validateEmptyString (UsernameInvalid "Username is empty")
        let lengthValid = validateStringLength 1 20 (UsernameInvalid "Username is too long") 

        let validateUsername un = notEmpty un <* lengthValid un  
        
        let loginUser un =
            match findUserFunction un with
            | Some user -> 
                match user.IsLoggedIn with
                | true -> Bad [ (UserLoggedIn "User with that username is already logged in") ]
                | false -> 
                    setLoggedInFunction username |> ok
            | None ->
                { Id = 0; Username = un; IsLoggedIn = true } 
                |> addUserFunction
                |> ok

        username |> validateUsername >>= loginUser

    let logout findUserFunction setLoggedOutFunction username =
        match findUserFunction username with 
        | Some _ -> 
            setLoggedOutFunction username
            |> ok 
        | None -> Bad [ (UsernameInvalid "This user does not exist") ]


module UserServiceWithDataAccess =
    open PinetreeChat.DataAccess.UserRepository    
    let login = UserService.login getUser addUser setLoggedIn
    let logout = UserService.logout getUser setLoggedOut

            
        