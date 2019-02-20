import * as React from "react";
import { AuthComponent } from "./authentication/auth";
import { connect } from "react-redux";
import { Action, Dispatch } from "redux";
import { AppActions } from "../state/actions";
import { ICombinedReducersEntries } from "../types/combinedReducersEntries";

export interface IPageComponentDescriptor extends IPageComponentProps, IPageComponentActions {
}

interface IPageComponentProps {
    isLoginActive?: boolean | null;
}

interface IPageComponentActions {
    activateLoginTabAction: () => void;
    activateSignUpTabAction: () => void;
    loginToServiceAction: () => void;
}

export class PageComponent extends React.Component<IPageComponentDescriptor> {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <React.Fragment>
                <AuthComponent
                    isLoginActive={this.props.isLoginActive}
                    activateLoginTabAction={this.props.activateLoginTabAction}
                    activateSignUpTabAction={this.props.activateSignUpTabAction}
                    loginToServiceAction={this.props.loginToServiceAction}
                />
            </React.Fragment>
        );
    }
}

const mapStateToProps: (state: ICombinedReducersEntries) => IPageComponentProps = (state) => {
    return {
        isLoginActive: state ? state.appReducer.isLoginActive : true
    }
};

const mapDispatchToProps: (dispatch: Dispatch<Action<number>>) => IPageComponentActions = dispatch => {
    return {
        activateLoginTabAction: () => {
            dispatch(AppActions.activateLoginTabAction())
        },
        activateSignUpTabAction: () => {
            dispatch(AppActions.activateSignUpTabAction())
        },
        loginToServiceAction: () => {
            // TODO: make AJAX request and then dispatch action there
            console.log(255);
        }
    }
}

export const ConnectedPageComponent: any = connect(
    mapStateToProps,
    mapDispatchToProps
)(PageComponent);
