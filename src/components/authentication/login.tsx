import * as React from "react";
import Axios from "axios";

export interface ILoginComponentDescriptor {

}

export interface ILoginComponentStateData {
    login: string;
    password: string;
}

export class LoginComponent extends React.Component<ILoginComponentDescriptor, ILoginComponentStateData> {
    constructor(props: ILoginComponentDescriptor) {
        super(props);
        this.state = {
            login: '',
            password: ''
        };

        this.handleLoginChange = this.handleLoginChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleClickLoginBtn = this.handleClickLoginBtn.bind(this);
    }

    render() {
        return (
            <div className="login-form-grid-container">
                <div className="grid-item grid-item-username-label">Username:</div>
                <div className="grid-item grid-item-username"><input type="text" value={this.state.login} onChange={this.handleLoginChange} /></div>
                <div className="grid-item grid-item-password-label">Password:</div>
                <div className="grid-item grid-item-password"><input type="password" value={this.state.password} onChange={this.handlePasswordChange} /></div>
                <div className="grid-item grid-item-button"><button onClick={this.handleClickLoginBtn}>Login</button></div>
            </div>
        );
    }

    private handleLoginChange(event: React.FormEvent<HTMLInputElement>): void {
        this.setState({ login: event.currentTarget.value });
    }

    private handlePasswordChange(event: React.FormEvent<HTMLInputElement>): void {
        this.setState({ password: event.currentTarget.value });
    }

    private handleClickLoginBtn(event: React.MouseEvent<HTMLButtonElement>): void {
        Axios.post(`/api/users/token`, {
            UserName: this.state.login,
            Password: this.state.password
        }).then(
            (response) => {
                console.log(response);
                this.cleanTheState();
                // TODO: Dispatch is here?
            },
            (error) => {
                // TODO: show notice on the top of the page or somewhere in the Login form
                this.cleanTheState();
            });
    }

    private cleanTheState(): void {
        this.setState({
            login: '',
            password: ''
        });
    }
}
