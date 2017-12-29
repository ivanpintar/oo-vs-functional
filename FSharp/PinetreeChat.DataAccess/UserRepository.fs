namespace PinetreeChat.DataAccess

module UserRepository =
    open PinetreeChat.Entities

    let mutable private userList = List.empty<User>
    
    let private updateUserInList updateFunction username =
        let mappingFunction user = 
            if (user.Username = username)
            then updateFunction user
            else user           
            
        userList <- List.map mappingFunction userList
        List.find (fun u -> u.Username = username) userList

    let getUsers () =
        userList 

    let getUser username =
        try List.find (fun u -> u.Username = username) userList |> Some
        with _ -> None

    let addUser user =
        userList <- user :: userList
        user

    let setLoggedIn =
        updateUserInList (fun u -> { u with IsLoggedIn = true })

    let setLoggedOut =
        updateUserInList (fun u -> { u with IsLoggedIn = false })
