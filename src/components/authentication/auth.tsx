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
                <div className="login-tabs-grid-container">
                    <div className={"tab-login grid-item" + (this.props.isLoginActive ? " active" : "")}
                        onClick={this.props.activateLoginTabAction}>LOGIN</div>
                    <div className={"tab-signup grid-item" + (!this.props.isLoginActive ? " active" : "")}
                        onClick={this.props.activateSignUpTabAction}>SIGN UP</div>
                </div>
                {this.props.isLoginActive ? <LoginComponent /> : <SignupComponent />}
            </div>
        );
    }
}