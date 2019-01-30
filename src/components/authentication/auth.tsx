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
            <div className="login-box">
                <div className="login-cases-grid-container">
                    <div className="login-cases-login grid-item" onClick={this.props.activateLoginTabAction}>Login</div>
                    <div className="login-cases-signup grid-item" onClick={this.props.activateSignUpTabAction}>Sign Up</div>
                </div>
                <div className="content">{this.props.isLoginActive ? <LoginComponent /> : <SignupComponent />}</div>
            </div>
        );
    }
}