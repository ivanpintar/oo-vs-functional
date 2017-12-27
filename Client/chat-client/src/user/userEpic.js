import { combineEpics } from 'redux-observable'
import { loggedInAction, loggedOutAction, userExistsAction, usernameInvalidAction } from './userActions'
import { Observable } from 'rxjs/Observable'
import { ajax } from 'rxjs/observable/dom/ajax'
import constants from '../constants'
import { serverErrorAction } from '../app/appActions';

const loginUrl = constants.apiUrl + 'user/login';
const logoutUrl = constants.apiUrl + 'user/logout';

const loginEpic = (action$) => 
    action$.ofType('USER.LOGIN')
        .mergeMap(a => 
            ajax.post(loginUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedInAction(a.username))
                .catch(error => {
                    switch(error.status){
                        case 409: return Observable.of(userExistsAction(a.username))
                        case 400: return Observable.of(usernameInvalidAction())
                        default: return Observable.of(serverErrorAction(error));
                    }
                })
        )

const logoutEpic = (action$) => 
    action$.ofType('USER.LOGOUT')
        .mergeMap(a => 
            ajax.post(logoutUrl, { username: a.username }, { 'Content-Type': 'application/json'})
                .map(response => loggedOutAction(a.username))
                .catch(error => Observable.of(serverErrorAction(error)))
        )

export default combineEpics(loginEpic, logoutEpic)