import { connect } from 'react-redux';
import ChuckNorris from './chuck_norris';

const mapStateToProps = ({ jokes }) => {
    const { text, error} = jokes;

    return {
        joke: text,
        error
    };
};

const ChuckNorrisContainer = connect(
    mapStateToProps
)(ChuckNorris);

export default ChuckNorrisContainer;