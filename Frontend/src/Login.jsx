import React from "react";
import "./auth.css";

export default function Login({ onLogin, onSwitchRegister }) {
	const [error, setError] = React.useState('');
	const [isSubmitting, setIsSubmitting] = React.useState(false);
	const handleSubmit = async (event) => {
		event.preventDefault();
		setError('');
		setIsSubmitting(true);
		const formData = new FormData(event.currentTarget);
		try {
			await onLogin({ email: formData.get('email'), password: formData.get('password') });
		} catch (submitError) {
			setError(submitError.message);
		} finally {
			setIsSubmitting(false);
		}
	};

	return (
		<main className="auth-page">
			<section className="auth-card">
				<div className="auth-logo" aria-label="Workout Tracker logo">
					WT
				</div>
				<h1 className="auth-title">Welcome back</h1>
				<p className="auth-subtitle">Log in to continue tracking your workouts.</p>

				<form className="auth-form" onSubmit={handleSubmit}>
					<label htmlFor="email" className="auth-label">
						Email
					</label>
					<input
						id="email"
						name="email"
						type="email"
						placeholder="you@example.com"
						required
						className="auth-input"
					/>

					<label htmlFor="password" className="auth-label">
						Password
					</label>
					<input
						id="password"
						name="password"
						type="password"
						placeholder="Enter your password"
						required
						className="auth-input"
					/>

					<button type="submit" className="auth-button" disabled={isSubmitting}>
						{isSubmitting ? "Logging in..." : "Log in"}
					</button>
					{error && <p role="alert" className="auth-error">{error}</p>}
				</form>

				<p className="auth-footer">
					Don&apos;t have an account? <button type="button" onClick={onSwitchRegister} className="auth-link">Sign up</button>
				</p>
			</section>
		</main>
	);
}

