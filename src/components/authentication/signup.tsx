import * as React from "react";

export interface ISignupComponentDescriptor {
    
}

export class SignupComponent extends React.Component<ISignupComponentDescriptor> {
    render() {
        return (
            <div className="signup-form-grid-container">Signing Up is temporary suspended. Only already registered users can use the service.</div>
        );
    }
}