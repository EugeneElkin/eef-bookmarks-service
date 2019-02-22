import * as React from "react";
import Axios from "axios";
import { LoginComponent } from "./login";
import { SignupComponent } from "./signup";
import { connect } from "react-redux";
import { AppActions } from "../../state/actions";
import { Action, Dispatch } from "redux";
import { ICombinedReducersEntries } from "../../types/combinedReducersEntries";

export interface IAuthComponentDescriptor extends IAuthComponentProps {
    handlers: IAuthComponentHandlers
}

interface IAuthComponentProps {
    isLoginActive?: boolean | null;
}

interface IAuthComponentHandlers {
    clickLoginTab: () => void;
    clickSignUpTab: () => void;
}

export class AuthComponent extends React.Component<IAuthComponentDescriptor> {
    render() {
        return (
            <div className="login-box">
                <div className="login-tabs-grid-container">
                    <div className={"tab-login grid-item" + (this.props.isLoginActive ? " active" : "")}
                        onClick={this.props.handlers.clickLoginTab}>LOGIN</div>
                    <div className={"tab-signup grid-item" + (!this.props.isLoginActive ? " active" : "")}
                        onClick={this.props.handlers.clickSignUpTab}>SIGN UP</div>
                </div>
                {this.props.isLoginActive ? <LoginComponent /> : <SignupComponent />}
            </div>
        );
    }
}

const mapStateToProps: (state: ICombinedReducersEntries) => IAuthComponentProps = (state) => {
    return {
        isLoginActive: state ? state.appReducer.isLoginActive : true
    }
};

const mapEventsToDispatch: (dispatch: Dispatch<Action<number>>) => IAuthComponentDescriptor = dispatch => {
    return {
        handlers: {
            clickLoginTab: () => {
                dispatch(AppActions.activateLoginTabAction())
            },
            clickSignUpTab: () => {
                dispatch(AppActions.activateSignUpTabAction())
            }
        }
    }
}

export const ConnectedAuthComponent: any = connect(
    mapStateToProps,
    mapEventsToDispatch
)(AuthComponent);