import { connect } from 'react-redux';
import ChuckNorris from './chuck_norris';
import { getJoke } from './store/actions';

const mapStateToProps = ({ jokes }) => {
    const { text, error } = jokes;

    return {
        joke: text,
        error
    };
};

const mapDispatchToProps = (dispatch) => ({
    getJoke: (id) => { dispatch(getJoke(id)); }
});

const ChuckNorrisContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(ChuckNorris);

export default ChuckNorrisContainer;