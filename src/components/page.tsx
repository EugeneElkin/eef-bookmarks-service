import * as React from "react";
import { AuthComponent } from "./authentication/auth";
import { connect } from "react-redux";
import { StateProps } from "../types/stateProps";

interface IPageComponentDescriptor {
    isLoginActive?: boolean | null;
}

class PageComponent extends React.Component<IPageComponentDescriptor> {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <AuthComponent
                isLoginActive={this.props.isLoginActive}
            />
        );
    }
}

const mapStateToProps: (state: StateProps) => IPageComponentDescriptor = (state) => {
    return {
        isLoginActive: state ? state.isLoginActive : true
    }
};

export const ConnectedPageComponent = connect(
    mapStateToProps
)(PageComponent);
