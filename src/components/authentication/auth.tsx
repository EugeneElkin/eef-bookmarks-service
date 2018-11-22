import * as React from "react";
import { LoginComponent } from "./login";
import { SignupComponent } from "./signup";
import { store } from "../..";
import { AppActions } from "../../actions";

export interface IAuthComponentDescriptor {
    isLoginActive?: boolean | null;
}

export class AuthComponent extends React.Component<IAuthComponentDescriptor> {
    render() {
        return (
            <div>
                Login Form!!!
                <div><a href="#" onClick={() => store.dispatch(AppActions.activateLoginTabAction())}>Login</a></div>
                <div><a href="#" onClick={() => store.dispatch(AppActions.activateSignUpTabAction())}>Sign Up</a></div>
                {this.props.isLoginActive ? <LoginComponent /> : <SignupComponent />}
            </div>
        );
    }
}