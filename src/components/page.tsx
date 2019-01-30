import * as React from "react";
import { AuthComponent } from "./authentication/auth";
import { connect } from "react-redux";
import { Action, Dispatch } from "redux";
import { AppActions } from "../state/actions";
import { CombinedReducersEntries } from "../types/combinedReducersEntries";

export interface IPageComponentDescriptor extends IPageComponentProps, IPageComponentActions {
}

interface IPageComponentProps {
    isLoginActive?: boolean | null;
}

interface IPageComponentActions {
    activateLoginTabAction: () => void;
    activateSignUpTabAction: () => void;
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
                />
            </React.Fragment>
        );
    }
}

const mapStateToProps: (state: CombinedReducersEntries) => IPageComponentProps = (state) => {
    console.log(state);
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
        }
    }
}

export const ConnectedPageComponent: any = connect(
    mapStateToProps,
    mapDispatchToProps
)(PageComponent);
