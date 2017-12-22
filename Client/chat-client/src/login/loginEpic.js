import { ofType } from 'redux-observable'
import { delay } from 'rxjs/operators'
import { loggedInAction, userExistsAction } from './loginActions'
import { Observable } from 'rxjs/Observable';
import 'rxjs/observable/dom/ajax'

export default (action$) => 
    action$.ofType('LOGIN.LOGIN')
        .mergeMap(a => {
            var username = '{ "username": "' + a.username + '"}';
            return Observable.ajax.post(window.constants.apiUrl + 'api/login', username, { 'Content-Type': 'application/json' })
                .map(response => {
                    return loggedInAction(a.username)
                })
                .catch(error => Observable.of(userExistsAction(a.username)))
        })