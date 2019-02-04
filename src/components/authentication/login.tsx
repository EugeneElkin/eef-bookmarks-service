import * as React from "react";

export interface ILoginComponentDescriptor {

}

export class LoginComponent extends React.Component<ILoginComponentDescriptor> {
    render() {
        return (
            <div className="login-form-grid-container">
                <div className="grid-item">Username:</div>
                <div className="grid-item"><input type="text" /></div>
                <div className="grid-item">Password:</div>
                <div className="grid-item"><input type="password" /></div>
                <div className="grid-item"></div>
                <div className="grid-item"><button>Login</button></div>
            </div>

        );
    }
}