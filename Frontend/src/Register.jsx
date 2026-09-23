import React from "react";
import "./auth.css";

export default function Register({ onRegister, onSwitchLogin }) {
    const [error, setError] = React.useState("");
    const [isSubmitting, setIsSubmitting] = React.useState(false);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const password = formData.get("password");

        if (password !== formData.get("passwordConfirmation")) {
            setError("Parolele nu coincid.");
            return;
        }

        setError("");
        setIsSubmitting(true);
        try {
            await onRegister({ username: formData.get("username"), email: formData.get("email"), password });
        } catch (submitError) {
            setError(submitError.message);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <main className="auth-page">
            <section className="auth-card">
                <div className="auth-logo" aria-label="Workout Tracker logo">WT</div>
                <h1 className="auth-title">Create account</h1>
                <p className="auth-subtitle">Register to start tracking your workouts.</p>

                <form className="auth-form" onSubmit={handleSubmit}>
                    <label htmlFor="username" className="auth-label">Username</label>
                    <input id="username" name="username" type="text" placeholder="Andrei" required className="auth-input" />
                    <label htmlFor="email" className="auth-label">Email</label>
                    <input id="email" name="email" type="email" placeholder="you@example.com" required className="auth-input" />
                    <label htmlFor="password" className="auth-label">Password</label>
                    <input id="password" name="password" type="password" placeholder="Enter your password" required className="auth-input" />
                    <label htmlFor="passwordConfirmation" className="auth-label">Repeat Password</label>
                    <input id="passwordConfirmation" name="passwordConfirmation" type="password" placeholder="Reenter your password" required className="auth-input" />
                    <button type="submit" className="auth-button" disabled={isSubmitting}>
                        {isSubmitting ? "Creating account..." : "Create account"}
                    </button>
                    {error && <p role="alert" className="auth-error">{error}</p>}
                </form>

                <p className="auth-footer">
                    Already have an account? <button type="button" onClick={onSwitchLogin} className="auth-link">Log in</button>
                </p>
            </section>
        </main>
    );
}

