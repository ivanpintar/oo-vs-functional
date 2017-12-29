namespace PinetreeChat.Domain

module Validation = 
    open Chessie.ErrorHandling
    open Chessie.ErrorHandling.Trial
    open System

    /// Composes two choice types, passing the case-1 type of the right value.
    let inline ( *> ) a b = lift2 (fun _ z -> z) a b

    /// Composes two choice types, passing the case-2 type of the left value.
    let inline (<*) a b = lift2 (fun z _ -> z) a b

    /// Composes a choice type with a non choice type.
    let inline (<?>) a b = lift2 (fun _ z -> z) a (ok b)

    /// Composes a non-choice type with a choice type.
    let inline (|?>) a b = lift2 (fun z _ -> z) (Bad a) b

    let inCase predicate error value =
        let result = predicate value
        match result with 
        | false -> ok value
        | true -> Bad [ error ]

    let validateEmptyString e v = inCase (fun s -> String.IsNullOrWhiteSpace(s)) e v
    let validateStringLength min max e v = inCase (fun s -> not (String.length s >= min && String.length s <= max)) e v
        
        

