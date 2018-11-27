import * as React from "react";
import { LoginComponent } from "./login";
import { SignupComponent } from "./signup";

export interface IAuthComponentDescriptor {
    isLoginActive?: boolean | null;
    activateLoginTabAction: () => void;
    activateSignUpTabAction: () => void;
}

export class AuthComponent extends React.Component<IAuthComponentDescriptor> {
    render() {
        return (
            <div>
                Login Form!!!
                <div><a href="#" onClick={this.props.activateLoginTabAction}>Login</a></div>
                <div><a href="#" onClick={this.props.activateSignUpTabAction}>Sign Up</a></div>
                {this.props.isLoginActive ? <LoginComponent /> : <SignupComponent />}
            </div>
        );
    }
}