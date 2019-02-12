import * as React from "react";

export interface ILoginComponentDescriptor {

}

export class LoginComponent extends React.Component<ILoginComponentDescriptor> {
    render() {
        return (
            <div className="login-form-grid-container">
                <div className="grid-item grid-item-username-label">Username:</div>
                <div className="grid-item grid-item-username"><input type="text" /></div>
                <div className="grid-item grid-item-password-label">Password:</div>
                <div className="grid-item grid-item-password"><input type="password" /></div>
                <div className="grid-item grid-item-button"><button>Login</button></div>
            </div>

        );
    }
}