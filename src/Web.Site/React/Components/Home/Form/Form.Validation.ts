import * as Yup from 'yup';
// eslint-disable-next-line no-unused-vars
import { WithTranslation } from 'react-i18next';

export default (props: WithTranslation): object => Yup.object().shape({
  Question1: Yup.string()
    .required(props.t('Form:question1.required')),
  Question2: Yup.string()
    .required(props.t('Form:question2.required')),
  Question3: Yup.string()
    .required(props.t('Form:question3.required')),
  Question4: Yup.string()
    .required(props.t('Form:question4.required')),
  Question5: Yup.string()
    .required(props.t('Form:question5.required')),
  Name: Yup.string()
    .required(props.t('Form:name.required')),
  Email: Yup.string()
    .required(props.t('Form:email.required'))
    .email(props.t('Form:email.email')),
});
