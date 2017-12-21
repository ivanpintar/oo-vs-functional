import { ofType } from 'redux-observable';
import { delay } from 'rxjs/operators';
import { loggedInAction } from './loginActions'

export default (action$) => 
    action$.ofType('LOGIN.LOGIN')
        .delay(1000)
        .map(a => loggedInAction(a.username))