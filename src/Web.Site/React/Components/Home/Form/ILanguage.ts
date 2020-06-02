export interface ILanguage {
  title: string,
  subtitle: string,
  question1: {
    title: string,
    text: string,
    required: string,
  },
  question2: {
    title: string,
    text: string,
    required: string,
  },
  question3: {
    title: string,
    text: string,
    required: string,
  },
  question4: {
    title: string,
    text: string,
    required: string,
  },
  question5: {
    title: string,
    text: string,
    required: string,
  },
  approved: {
    label: string,
    checked: string,
    unchecked: string,
  },
  name: {
    title: string,
    text: string,
    required: string,
  },
  email: {
    title: string,
    text: string,
    required: string,
    email: string,
  }
  button: {
    title: string,
    text: string,
  },
  feedback: {
    success: string,
    failure: string,
  },
}
