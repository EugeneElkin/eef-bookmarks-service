import { Reducer } from "redux";
import { StateProps } from "./types/stateProps";
import { ActionType } from "./actions";

export const rootReducer: Reducer = (state: StateProps, action: any) => {
    switch (action.type) {
        case ActionType.ACTIVATE_SIGNUP_TAB:
            const result1 = {
                ...state,
                isLoginActive: false
            };
            console.log(result1);
            return result1;
        case ActionType.ACTIVATE_LOGIN_TAB:
            const result2 = {
                ...state,
                isLoginActive: true
            }
            console.log(result2);
            return result2;
        default:
            return state;
    }
};