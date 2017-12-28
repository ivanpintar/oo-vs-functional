namespace PinetreeChat.DataAccess

module UserRepository =
    open PinetreeChat.Entities

    let mutable private userList = List.empty<User>
    
    let private updateUserInList username updateFunction =
        let mappingFunction user = 
            if (user.Username = username)
            then updateFunction user
            else user           
            
        List.map mappingFunction userList

    let getUsers () =
        userList 

    let addUser (user:User) =
        userList <- user :: userList

    let setLoggedIn username =
        let updateFunction user = { user with IsLoggedIn = true }
        userList <- updateUserInList username updateFunction

    let setLoggedOut username =
        let updateFunction user = { user with IsLoggedIn = false }
        userList <- updateUserInList username updateFunction
